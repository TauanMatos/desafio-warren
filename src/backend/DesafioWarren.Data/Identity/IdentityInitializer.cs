using DesafioWarren.Data.Context;
using DesafioWarren.Model.Dtos;
using DesafioWarren.Model.Entities;
using DesafioWarren.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace DesafioWarren.Data.Identity
{
    public class IdentityInitializer
    {
        private readonly WarrenDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;


        public IdentityInitializer(
            WarrenDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAccountService accountService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                if (!_roleManager.RoleExistsAsync(Roles.CLIENT_ROLE).Result)
                {
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(Roles.CLIENT_ROLE)).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.CLIENT_ROLE}.");
                    }
                }

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "client1",
                        Email = "client1@teste.com",
                        EmailConfirmed = true,
                        Client = new Client()
                        {
                            Name = "Cliente 1",
                            Address = "Endereço Cliente 1",
                            CPF = "123.132.132-9",
                            Phone = "99 999999999",
                            Gender = Model.Entities.Enum.Gender.Masculino,
                            Account = new Account()
                        }
                    },
                    "Client@1", Roles.CLIENT_ROLE, true);

                CreateUser(
                    new ApplicationUser()
                    {
                        UserName = "client2",
                        Email = "client2@teste.com",
                        EmailConfirmed = true,
                        Client = new Client()
                        {
                            Name = "Cliente 2",
                            Address = "Endereço Cliente 2",
                            CPF = "123.132.132-9",
                            Phone = "99 999999999",
                            Gender = Model.Entities.Enum.Gender.Masculino,
                            Account = new Account()
                        }
                    },
                    "Client@2", Roles.CLIENT_ROLE);
            }
        }

        private void CreateUser(
            ApplicationUser user,
            string password,
            string initialRole = null,
            bool makeDeposit = false)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }

            if (makeDeposit)
                this.MakeDeposit(user.UserName);
        }

        private void MakeDeposit(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            this._accountService.Deposit(new AccountRequestDto() { ClientId = user.ClientId, Value = 10000 });            
        }
    }
}
