using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Security.Claims;
using IdentityServer3.Core.Models;
using Xunit;

namespace xunit.tests
{
    public class TestClient
    {
        [Fact(DisplayName = "Should create map with no exception")]
        public void TestAddMap()
        {
            Mapper.CreateMap<IdentityServer3.EntityFramework.Entities.Scope, IdentityServer3.Core.Models.Scope>(MemberList.Destination)
                .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)));
            Mapper.CreateMap<IdentityServer3.EntityFramework.Entities.ScopeClaim, IdentityServer3.Core.Models.ScopeClaim>(MemberList.Destination);

            Mapper.CreateMap<IdentityServer3.EntityFramework.Entities.ClientSecret, IdentityServer3.Core.Models.Secret>(MemberList.Destination);
            Mapper.CreateMap<IdentityServer3.EntityFramework.Entities.Client, IdentityServer3.Core.Models.Client>(MemberList.Destination)
                .ForMember(x => x.UpdateAccessTokenClaimsOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenOnRefresh))
                .ForMember(x => x.AllowAccessToAllCustomGrantTypes, opt => opt.MapFrom(src => src.AllowAccessToAllGrantTypes))
                .ForMember(x => x.AllowedCustomGrantTypes, opt => opt.MapFrom(src => src.AllowedCustomGrantTypes.Select(x => x.GrantType)))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope)))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))));
        }

        [Fact(DisplayName = "Should map client to EF Client")]
        public void TestAddClient()
        {

            

            var entity = new Client()
            {
                ClientId = "test",
                ClientName = "test2",
                AllowedScopes = new List<string> { "item1", "item2" },
                RedirectUris = new List<string> { "http://redirect1" },
                AccessTokenType = AccessTokenType.Jwt,
                Flow = Flows.ResourceOwner,
                ClientSecrets = new List<Secret> { new Secret("secret") }


            }.ToEntity();
            Assert.NotNull(entity);
        }
    }
}
