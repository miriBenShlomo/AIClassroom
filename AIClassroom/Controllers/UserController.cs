using AIClassroom.BL.API;
using AIClassroom.BL.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AIClassroom.Controllers
{
    /// <summary>
    /// Manages user-related operations such as registration, profile retrieval, and learning history.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPromptService _promptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The service for user operations.</param>
        /// <param name="promptService">The service for prompt-related operations.</param>
        public UsersController(IUserService userService, IPromptService promptService)
        {
            _userService = userService;
            _promptService = promptService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userRegistrationDto">The user's name and phone number.</param>
        /// <returns>A 201 Created response with the newly created user object, including their ID.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new UserDto
            {
                Name = userRegistrationDto.Name,
                Phone = userRegistrationDto.Phone
            };

            var newUser = await _userService.AddUserAsync(userDto);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        /// <summary>
        /// Gets a specific user by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user object if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Gets the entire learning history (all prompts) for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose history is requested.</param>
        /// <returns>A list of prompts created by the specified user.</returns>
        [HttpGet("{userId}/prompts")]
        [ProducesResponseType(typeof(IEnumerable<PromptDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PromptDto>>> GetUserPromptHistory(int userId)
        {
            var prompts = await _promptService.GetLearningHistoryByUserIdAsync(userId);
            return Ok(prompts);
        }

        /// <summary>
        /// Gets a list of all users. (Intended for Admin Dashboard)
        /// </summary>
        /// <returns>A list of all users in the system.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}