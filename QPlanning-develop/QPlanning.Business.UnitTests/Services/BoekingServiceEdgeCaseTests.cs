using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Repositories.Gateway;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.Services;

public class BoekingServiceEdgeCaseTests
{
    private BoekingService CreateService()
    {
        var mockRepo = new Mock<IBoekingRepository>();
        mockRepo.Setup(r => r.AddBoeking(It.IsAny<DomainModelBoeking>()))
            .ReturnsAsync(new BaseResponse("1", true));
        var mockMedewerkerRepo = new Mock<IMedewerkerRepository>();
        return new BoekingService(mockRepo.Object, mockMedewerkerRepo.Object);
    }

    [Fact]
    public async Task AddBoeking_With_Weeknummer_Zero_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boeking = new DomainModelBoeking { Jaar = 2025, Weeknummer = 0, Uren = 8, MedewerkerId = 1 };

        // Act
        var result = await service.AddBoeking(boeking);

        // Assert
        Assert.False(result.Success); // Should fail if validation is added!
    }

    [Fact]
    public async Task AddBoeking_With_Weeknummer_Over_53_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boeking = new DomainModelBoeking { Jaar = 2025, Weeknummer = 54, Uren = 8, MedewerkerId = 1 };

        // Act
        var result = await service.AddBoeking(boeking);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Negative_Uren_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boeking = new DomainModelBoeking { Jaar = 2025, Weeknummer = 10, Uren = -5, MedewerkerId = 1 };

        // Act
        var result = await service.AddBoeking(boeking);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Missing_Jaar_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boeking = new DomainModelBoeking { Weeknummer = 10, Uren = 8, MedewerkerId = 1 };

        // Act
        var result = await service.AddBoeking(new DomainModelBoeking
        {
            Weeknummer = 10,
            Uren = 8,
            MedewerkerId = 1
        });

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Date_Not_Matching_Year_Week_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boeking = new DomainModelBoeking
        {
            Jaar = 2025,
            Weeknummer = 10,
            Uren = 8,
            MedewerkerId = 1,
            Datum = new DateTime(2024, 1, 1) // Not in 2025, week 10
        };

        // Act
        var result = await service.AddBoeking(boeking);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoekingen_With_Empty_List_Should_Fail()
    {
        // Arrange
        var service = CreateService();
        var boekingen = new List<DomainModelBoeking>();

        // Act
        var result = await service.AddBoekingen(boekingen);

        // Assert
        Assert.False(result.Success);
    }
}