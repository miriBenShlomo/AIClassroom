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
        /// Logs in a user using name and phone.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> LoginUser([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByNameAndPhoneAsync(loginDto.Name, loginDto.Phone);

            if (user == null)
                return Unauthorized("User not found or credentials incorrect");

            return Ok(user);
        }

        /// <summary>
        /// Gets a specific user by their ID.
        /// </summary>
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
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}