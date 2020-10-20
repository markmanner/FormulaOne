using System;
using Microsoft.AspNetCore.Mvc;
using FormulaOne.Core.Interfaces.IServices;
using AutoMapper;
using FormulaOne.Web.ViewModels.FormulaOneTeam;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using FormulaOne.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace FormulaOne.Web.Controllers
{
    public class FormulaOneTeamsController : Controller
    {
        /// <summary>
        /// Formula One Team service.
        /// </summary>
        private readonly IFormulaOneTeamService _service;

        /// <summary>
        /// Action Logger.
        /// </summary>
        private readonly ILogger<FormulaOneTeamsController> _logger;

        /// <summary>
        /// AutoMapper.
        /// </summary>
        private readonly IMapper _mapper;

        public FormulaOneTeamsController(IFormulaOneTeamService service, IMapper mapper, ILogger<FormulaOneTeamsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var teams = _service.GetAll();
            var teamViewModels = _mapper.Map<IEnumerable<FormulaOneTeamViewModel>>(teams);

            return View(teamViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormulaOneTeamViewModel formulaOneTeamViewModel)
        {
            if (ModelState.IsValid)
            {
                var formulaOneTeamDTO = _mapper.Map<FormulaOneTeamDTO>(formulaOneTeamViewModel);

                _service.Add(formulaOneTeamDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(formulaOneTeamViewModel);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulaOneTeamDTO = _service.GetById(id.Value);
            if (formulaOneTeamDTO == null)
            {
                return NotFound();
            }

            var formulaOneTeamViewModel = _mapper.Map<FormulaOneTeamViewModel>(formulaOneTeamDTO);

            return View(formulaOneTeamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FormulaOneTeamViewModel formulaOneTeamViewModel)
        {
            if (ModelState.IsValid)
            {
                var formulaOneTeamDTO = _mapper.Map<FormulaOneTeamDTO>(formulaOneTeamViewModel);
                _service.Update(formulaOneTeamDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(formulaOneTeamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            
            return Ok();
        }
    }
}
