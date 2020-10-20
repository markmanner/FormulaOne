using AutoMapper;
using FormulaOne.Core.DTOs;
using FormulaOne.Core.Interfaces.IRepositories;
using FormulaOne.Core.Models;
using FormulaOne.Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FormulaOne.Infrastructure.Tests
{
    public class FormulaOneTeamServiceTests
    {
        private readonly Mock<IFormulaOneTeamRepository> _repositoryMock;
        private readonly FormulaOneTeamService _service;
        private readonly Mock<IMapper> _mapperMock;

        public FormulaOneTeamServiceTests()
        {
            _repositoryMock = new Mock<IFormulaOneTeamRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new FormulaOneTeamService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllItems_WhenItemsAreExist()
        {
            List<FormulaOneTeam> formulaOneTeamList = MockFormulaOneTeamListWithTwoItem();
            List<FormulaOneTeamDTO> expectedList = MockFormulaOneTeamDTOListWithTwoItem();
            _repositoryMock.Setup(s => s.GetAll()).Returns(formulaOneTeamList);
            _mapperMock.Setup(s => s.Map<IEnumerable<FormulaOneTeamDTO>>(formulaOneTeamList)).Returns(expectedList);

            var result = _service.GetAll().ToList();

            Assert.Equal(expectedList.Count(), result.Count());
            Assert.Equal(expectedList[0].Id, result[0].Id);
            Assert.Equal(expectedList[0].Name, result[0].Name);
            Assert.Equal(expectedList[0].FoundationYear, result[0].FoundationYear);
            Assert.Equal(expectedList[0].NumberOfChampionshipWon, result[0].NumberOfChampionshipWon);
            Assert.Equal(expectedList[0].IsEntryFeePaid, result[0].IsEntryFeePaid);
        }

        [Fact]
        public void GetById_ShouldReturnFormulaOneTeamDTO_WhenIdIsValid()
        {
            FormulaOneTeam formulaOneTeam = MockFormulaOneTeam();
            Guid teamId = formulaOneTeam.Id;
            FormulaOneTeamDTO expectedResult = MockFormulaOneTeamDTO();
            _repositoryMock.Setup(s => s.GetById(teamId)).Returns(formulaOneTeam);
            _mapperMock.Setup(s => s.Map<FormulaOneTeamDTO>(formulaOneTeam)).Returns(expectedResult);


            var result = _service.GetById(teamId);

            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.FoundationYear, result.FoundationYear);
            Assert.Equal(expectedResult.NumberOfChampionshipWon, result.NumberOfChampionshipWon);
            Assert.Equal(expectedResult.IsEntryFeePaid, result.IsEntryFeePaid);
        }

        [Fact]
        public void Add_CallRepositoryInsertMethodOnceWithModel_WhenParameterIsValid()
        {
            FormulaOneTeamDTO teamDTO = MockFormulaOneTeamDTO();
            FormulaOneTeam team = MockFormulaOneTeam();
            _mapperMock.Setup(s=>s.Map<FormulaOneTeam>(teamDTO)).Returns(team);

            _service.Add(teamDTO);

            _repositoryMock.Verify(c => c.Insert(team), Times.Once);
        }

        /// <summary>
        /// Get formula one team model
        /// </summary>
        /// <returns>FormulaOneTeam</returns>
        private FormulaOneTeam MockFormulaOneTeam()
        {
            return new FormulaOneTeam()
            {
                Id = Guid.Parse("E72CFA70-521B-42AA-976C-50656BE4B442"),
                Name = "team1",
                FoundationYear = 1990,
                IsEntryFeePaid = true,
                NumberOfChampionshipWon = 11,
                IsDeleted = false
            };
        }

        /// <summary>
        /// Get formula one team DTO
        /// </summary>
        /// <returns>FormulaOneTeamDTO</returns>
        private FormulaOneTeamDTO MockFormulaOneTeamDTO()
        {
            return new FormulaOneTeamDTO()
            {
                Id = Guid.Parse("E72CFA70-521B-42AA-976C-50656BE4B442"),
                Name = "team1",
                FoundationYear = 1990,
                IsEntryFeePaid = true,
                NumberOfChampionshipWon = 11,
            };
        }

        /// <summary>
        /// Get formula one team list with two items
        /// </summary>
        /// <returns>Return formula one team list with two items</returns>
        private List<FormulaOneTeam> MockFormulaOneTeamListWithTwoItem()
        {
            return new List<FormulaOneTeam>()
            {
                new FormulaOneTeam()
                {
                    Id = Guid.Parse("E72CFA70-521B-42AA-976C-50656BE4B442"),
                    Name = "team1",
                    FoundationYear = 1990,
                    IsEntryFeePaid = true,
                    NumberOfChampionshipWon = 11,
                    IsDeleted = false
                },
                new FormulaOneTeam()
                {
                    Id = Guid.Parse("72D61627-1FF0-42B5-8C16-F7713B7C27AF"),
                    Name = "team2",
                    FoundationYear = 1990,
                    IsEntryFeePaid = false,
                    NumberOfChampionshipWon = 10,
                    IsDeleted = false
                }
            };
        }

        /// <summary>
        /// Get formula one team DTO list with two items
        /// </summary>
        /// <returns>Return formula one team DTO list with two items</returns>
        private List<FormulaOneTeamDTO> MockFormulaOneTeamDTOListWithTwoItem()
        {
            return new List<FormulaOneTeamDTO>()
            {
                new FormulaOneTeamDTO()
                {
                    Id = Guid.Parse("E72CFA70-521B-42AA-976C-50656BE4B442"),
                    Name = "team1",
                    FoundationYear = 1990,
                    IsEntryFeePaid = true,
                    NumberOfChampionshipWon = 11,
                },
                new FormulaOneTeamDTO()
                {
                    Id = Guid.Parse("72D61627-1FF0-42B5-8C16-F7713B7C27AF"),
                    Name = "team2",
                    FoundationYear = 1990,
                    IsEntryFeePaid = false,
                    NumberOfChampionshipWon = 10,
                }
            };
        }
    }
}
