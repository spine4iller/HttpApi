using Api.Infra.Profiles;
using AutoMapper;

namespace Api.Tests
{
	public class MappingProfileTests
	{
		[Fact]
		public void MappingProfile_WhenMappingIncorrect_ThrowsException()
		{
			var config = new MapperConfiguration(cfg =>
				cfg.AddProfile<MappingProfile>());
			config.AssertConfigurationIsValid();
		}
	}
}
