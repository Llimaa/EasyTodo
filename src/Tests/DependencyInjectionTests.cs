using EasyTodoTests.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.Extensions.Configuration;
using Infrastructure.Config;
using Application.TodoAggregate;
using Infrastructure.Repositories;
using Infrastructure;

namespace EasyTodoTests;

public class DependencyInjectionTests 
{
    [Fact]
    public void It_Should_Dependence_Inject_As_Transient()
    {
        var mockIConfigurationSection = new Mock<IConfigurationSection>();
        mockIConfigurationSection.Setup(x => x.Key).Returns(nameof(TodoDbConfig));
        
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(_ => _.GetSection(nameof(TodoDbConfig))).Returns(mockIConfigurationSection.Object);

        var mockedServiceCollection = new Mock<IServiceCollection>();
        mockedServiceCollection.Object.AddTodo(configurationMock.Object);

        mockedServiceCollection.Verify(_ => _.Add(It.Is<ServiceDescriptor>(descriptor =>
            descriptor.Is<ITodoRepository, TodoRepository>(ServiceLifetime.Transient))));
    }


    [Fact]
    public void It_Should_Dependence_Inject_As_Singleton()
    {
        var mockIConfigurationSection = new Mock<IConfigurationSection>();
        mockIConfigurationSection.Setup(x => x.Key).Returns(nameof(TodoDbConfig));
        
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(_ => _.GetSection(nameof(TodoDbConfig))).Returns(mockIConfigurationSection.Object);

        var mockedServiceCollection = new Mock<IServiceCollection>();
        mockedServiceCollection.Object.AddTodo(configurationMock.Object);

        mockedServiceCollection.Verify(_ => _.Add(It.Is<ServiceDescriptor>(descriptor =>
            descriptor.Is<ITodoDbContext, TodoDbContext>(ServiceLifetime.Singleton))));
    }
}