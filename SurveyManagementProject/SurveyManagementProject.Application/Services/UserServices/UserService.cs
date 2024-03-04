using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SurveyManagementProject.Application.Abstractions.IRepositories;
using SurveyManagementProject.Application.Abstractions.IServises;
using SurveyManagementProject.Domain.Entities.DTOs;
using SurveyManagementProject.Domain.Entities.Exceptions;
using SurveyManagementProject.Domain.Entities.Models;

namespace SurveyManagementProject.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Create(UserDTO userDTO)
        {
            var email = await _userRepository.GetByAny(x => x.Email == userDTO.Email);
            var username = await _userRepository.GetByAny(x => x.Name == userDTO.Name);
            if (username == null)
            {
                if (email == null)
                {
                    var user = new User()
                    {
                        Name = userDTO.Name,
                        Email = userDTO.Email,
                        Password = userDTO.Password,
                        Role = userDTO.Role,
                    };
                    var result = await _userRepository.Create(user);

                    return "You succesfully registered!";
                }
                return "This email already exists";
            }
            return "This username already exists";
        }

        public async Task<string> Delete(int id)
        {
            var result = await _userRepository.Delete(x => x.Id == id);
            if (result)
            {
                return "Deleted";
            }
            else
            {
                throw new UserNotFoundException("User not found!");
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAll();

            var result = users.Select(model => new User
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
            });

            return result;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _userRepository.GetByAny(x => x.Email == email);
            if (result != null)
            {
                return result;
            }
            throw new UserNotFoundException("User not found!");
        }

        public async Task<User> GetById(int Id)
        {
            var result = await _userRepository.GetByAny(x => x.Id == Id);
            if (result != null)
            {
                return result;
            }
            throw new UserNotFoundException("User not found!");
        }

        public async Task<User> GetByName(string name)
        {
            var result = await _userRepository.GetByAny(d => d.Name == name);
            if (result != null)
            {
                return result;
            }
            throw new UserNotFoundException("User not found!");
        }

        public async Task<string> Update(int Id, UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();
            var email = res.Any(x => x.Email == userDTO.Email);
            var name = res.Any(x => x.Name == userDTO.Name);
            if (!email)
            {
                if (!name)
                {
                    var old = await _userRepository.GetByAny(x => x.Id == Id);

                    if (old == null) return "Failed";
                    old.Name = userDTO.Name;
                    old.Password = userDTO.Password;
                    old.Email = userDTO.Email;
                    old.Role = userDTO.Role;


                    await _userRepository.Update(old);
                    return "Updated";

                }
                return "Such login already exists";
            }
            return "Such email already exists";
        }

        public async Task<string> GetPdfPath()
        {

            var text = "";

            var getall = await _userRepository.GetAll();
            foreach (var user in getall.Where(x => x.Role != "Admin"))
            {
                text = text + $"{user.Name}|{user.Email}\n";
            }

            DirectoryInfo projectDirectoryInfo =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            var file = Guid.NewGuid().ToString();

            string pdfsFolder = Directory.CreateDirectory(
                 Path.Combine(projectDirectoryInfo.FullName, "pdfs")).FullName;

            QuestPDF.Settings.License = LicenseType.Community;

            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                      .Text("Library Users")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);

                          x.Item().Text(text);
                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
            .GeneratePdf(Path.Combine(pdfsFolder, $"{file}.pdf"));
            return Path.Combine(pdfsFolder, $"{file}.pdf");
        }
    }
}
