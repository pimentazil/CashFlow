using AutoMapper;
using CashFlow.Application.AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infrastructure.DataAccess;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users
{
    public class RegisterUserUseCase
    {

        private readonly CashFlowDbContext _ctx;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IMapper _mapper;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IAccessTokenGenerator _tokenGenerator;

        public RegisterUserUseCase(
            CashFlowDbContext context,
            IPasswordEncripter passwordEncripter,
            IMapper mapper,
            IUserReadOnlyRepository userReadOnlyRepository,
            IAccessTokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _ctx = context;
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncripter = passwordEncripter;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            _ctx.Users.Add(user);
            _ctx.SaveChanges();

            var response = new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };

            return response;
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var result = new RegisterUserValidator().Validate(request);

            var emailExiste = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExiste)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, "Email ja existe."));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOrValidationException(errorMessages);
            }
        }
    }
}