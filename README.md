# LightImage.Interactions
MediatR-powered interactions in view models.

[![Interactions](https://img.shields.io/nuget/v/lightimage.interactions)](https://www.nuget.org/packages/LightImage.Interactions/)

## Introduction

View models allow developers to separate user interface logic from the actual interface definition. Various awesome libraries like [ReactiveUI](https://reactiveui.net/) provide a great foundation for both the view model and view layers. When it comes to showing message boxes and input dialogs, it often becomes tricky to avoid the view model becoming too much dependent on the view layer. ReactiveUI solves this by letting the view model expose an `Interaction` and letting the view register a handler. The downside is that now the view showing this particular view model has become dependent on the mechanism for handling the interaction.

This small library provides an alternative using dependency injection and the [Mediatr library](https://github.com/jbogard/MediatR). View models depend on an `IInteractionService` for handling interactions. Under the hood, this uses the `IMediator` interface to map interaction view models to handlers defined in the view layer.

For example, let's assume we have a view model representing a simple document editor. The view model exposes a command for inserting an image. For this, the user needs to choose an image source. With ReactiveUI, the editor view would typically register an interaction handler by using some image picker dialog, thereby introducing a dependency between the two. If some other view also requires choosing an image, the same handler would need to be registered. Using this library, instead you can use dependency injection to register a dialog as a handler for this type of interaction.

## Basic usage

* Setup [MediatR](https://github.com/jbogard/MediatR) together with the dependency injection container of your choice, for example [Autofac](https://autofac.org/).
* Install the `LightImage.Interactions` package in the project containing your view models. Use the extension methods below in your view models, or define customer interactions as separate view models.
* Install the `LightImage.Interactions.WPF` package in the package containing your views, or implement your own dialogs.
* In the dependency injection container, make sure to
  - register the `InteractionService` class
  - register the interaction handlers

## Autofac sample

If you're using Autofac and WPF, the specific steps would be as follows:

* Add the following packages:
  - [Autofac](https://www.nuget.org/packages/Autofac/)
  - [MediatR](https://www.nuget.org/packages/MediatR/)
  - [MediatR.Extensions.Autofac.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Autofac.DependencyInjection)
  - [LightImage.Interactions](https://www.nuget.org/packages/LightImage.Interactions/)
  - [LightImage.Interactions.WPF](https://www.nuget.org/packages/LightImage.Interactions.WPF/)
* Setup the dependency injection container:
  - Register the interaction service using  `builder.RegisterType<InteractionService>().As<IInteractionService>().SingleInstance();`  
  - Register the handlers using `builder.AddMediatR(typeof(FileInteractionHandler).Assembly, GetType().Assembly);`
  - Make sure to enable contravariant handler resolution using `builder.RegisterSource(new ContravariantRegistrationSource());`

## Standard interactions

Various extension methods for the `IInteractionService` make it a lot easier to perform common user interactions without creating custom view models. Below is a list of these methods. The last column indicate which view models are being used. This defines the required interaction handler. For all methods below, standard handlers can be found in the `LightImage.Interactions.WPF` package. By using this package, you can therefore just use the methods without constructing the view models mentioned in the last column.

| Method signature | Description | View models |
|------------------|-------------|-------------|--------------|
`Show<TInput, TOutput>(input)` | Main method exposed by the interface. It takes any input view model `TInput` that implements `IInteractionInput<TOutput>` | `TInput` / `TOutput` |
| `ShowMessage(title, message, icon, buttons)` | Shows a message in a popup dialog and returns the chosen button. | `MessageOptions` / `MessageResult` |
| `YesNo(title, message, icon)` | Shorthand that displays a pair of *Yes*- and *No*-buttons and returns a boolean indicating whether *Yes* was chosen. | `MessageOptions` / `MessageResult` |
| `YesNoCancel(title, message, icon)` | As above, including a *Cancel*-button. The return type is `bool?` where `null` represents the *Cancel*-button. | `MessageOptions` / `MessageResult` |
| `Prompt<T>(title, message, defaultValue, icon, parser, formatter, predicate)` | General method for asking the user for input. The `parser` and `formatter` together perform conversion from the input type `T` to strings. The `predicate` defines which values are accepted. | `PromptOptions` / `PromptResult` |
| `Input(title, message, defaultValue, icon, predicate)` | Shorthand version of the above method. There are two versions, one for `string` return types and one for `int?`. | `PromptOptions` / `PromptResult` |
| `OpenFile()` | Allows for picking one or more files to be opened. | `OpenFileInput` / `OpenFileOutput` |
| `SaveFile()` | Allows for picking a path for saving a file. | `SaveFileInput` /`SaveFileOutput` |
| `Show<TEnum>(title, message)` | Allows for picking one member of an enum type. The `[Display]` and `[Description]` attributes can be used to decorate each member. The result is either the chosen member of `null` if *Cancel* was clicked. | `EnumViewModel` / `EnumMemberViewModel` |

## Custom interactions


## Tasks

## Unit tests

## Roadmap

* Complete documentation
* Add unit tests
