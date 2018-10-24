﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Translator.Services;

namespace Translator.ViewModels.Validations
{
    public class WordViewModelValidator : AbstractValidator<WordViewModel>
    {
        public WordViewModelValidator()
        {
            RuleFor(viewModel => viewModel.Original)
                .NotEmpty()
                .WithMessage(ConstantService.ErrorMessages.EmptyWord);

            RuleFor(viewModel => viewModel)
                .Must(viewModel => !viewModel.RootViewModel.Items.Any(element => element.Original == viewModel.Original))
                .WithMessage(ConstantService.ErrorMessages.WordAlreadyExist);

            RuleFor(viewModel => viewModel.Translations)
                .Must(viewModels => viewModels.All(viewModel=>!string.IsNullOrEmpty(viewModel.Value)))
                .WithMessage(ConstantService.ErrorMessages.EmptyTranslation);
        }
    }
}
