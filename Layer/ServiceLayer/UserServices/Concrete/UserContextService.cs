using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ServiceLayer.UserServices.Concrete
{
    
    public class UserContextService
    {
      
        private readonly AuthenticationStateProvider _authState;

        public UserContextService(AuthenticationStateProvider authState)
        {
            _authState = authState;
        }

     

        public async Task<int> GetLocationId()
        {
            var authState = await _authState.GetAuthenticationStateAsync();

            if (_authState == null)
            {
                throw new InvalidOperationException("Authentication state not initialized.");
            }

            foreach (var claim in authState.User.Claims)
            {
                if (claim.Type == "StoreId")
                {
                    return Convert.ToInt32(claim.Value);
                }
            }

            return 0; // Handle the case where LocationId claim is not found
        }
    }
}
