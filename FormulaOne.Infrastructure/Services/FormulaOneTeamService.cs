using AutoMapper;
using FormulaOne.Core.DTOs;
using FormulaOne.Core.Interfaces.IRepositories;
using FormulaOne.Core.Interfaces.IServices;
using FormulaOne.Core.Models;
using System;
using System.Collections.Generic;

namespace FormulaOne.Infrastructure.Services
{
    public class FormulaOneTeamService : IFormulaOneTeamService
    {
        /// <summary>
        /// Formula One team repository.
        /// </summary>
        private readonly IFormulaOneTeamRepository _repository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        public FormulaOneTeamService(IFormulaOneTeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<FormulaOneTeamDTO> GetAll()
        {
            var teamModels = _repository.GetAll();

            return _mapper.Map<IEnumerable<FormulaOneTeamDTO>>(teamModels);
        }

        public FormulaOneTeamDTO GetById(Guid id)
        {
            var team = _repository.GetById(id);

            return _mapper.Map<FormulaOneTeamDTO>(team);
        }

        public void Add(FormulaOneTeamDTO team)
        {
            var teamModel = _mapper.Map<FormulaOneTeam>(team);

            teamModel.IsDeleted = false;

            _repository.Insert(teamModel);
        }

        public void Update(FormulaOneTeamDTO team)
        {
            var teamModel = _mapper.Map<FormulaOneTeam>(team);

            _repository.Update(teamModel);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
