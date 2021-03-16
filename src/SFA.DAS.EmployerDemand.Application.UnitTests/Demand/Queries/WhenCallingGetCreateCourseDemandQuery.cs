using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerDemand.Application.Demand.Queries;
using SFA.DAS.EmployerDemand.Domain.Demand;
using SFA.DAS.EmployerDemand.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EmployerDemand.Application.UnitTests.Demand.Queries
{
    public class WhenCallingGetCreateCourseDemandQuery
    {
        [Test, MoqAutoData]
        public async Task Then_The_Service_Is_Called_And_Data_Returned(
            GetCreateCourseDemandQuery query,
            TrainingCourse course,
            [Frozen] Mock<IDemandService> service,
            GetCreateCourseDemandQueryHandler handler)
        {
            //Arrange
            service.Setup(x => x.GetCreateCourseDemand(query.TrainingCourseId))
                .ReturnsAsync(course);
            
            //Act
            var actual = await handler.Handle(query, CancellationToken.None);
            
            //Assert
            actual.TrainingCourse.Should().BeEquivalentTo(course);
        }
    }
}