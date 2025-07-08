# Urban UI - Transform Your WPF Applications with Modern Design

Welcome to Urban UI, your go-to UI Library Tool for breathing new life into WPF applications! Say goodbye to mundane designs and hello to a fresh, modern look that seamlessly integrates with conventional WPF controls. Elevate both functionality and user engagement with Urban UI.

🚧 **Work in Progress! More Controls Coming Soon** 🚧

We're constantly evolving to bring you even more stunning designs and enhanced features. Stay tuned for updates as we continue to expand our repertoire of supported controls.

![NuGet](https://img.shields.io/nuget/v/UrbanUI.Controls.svg)
![NuGet Downloads](https://img.shields.io/nuget/dt/UrbanUI.Controls.svg)
![License](https://img.shields.io/badge/license-MIT-green)

## Features:

- **Sleek Aesthetics:** Modernize your WPF applications with cutting-edge designs that captivate users.

- **Seamless Integration:** Effortlessly enhance conventional WPF controls with Urban UI's intuitive library.

- **Custom Theming - JSON Style:** Take full control of UI theming with our unique JSON-based theming system. Craft and tailor the visual experience of your application according to your preferences.

- **Improved User Engagement:** Elevate user experience by combining functionality with a visually appealing interface.

- **Snap Layout Support:** Enjoy advanced layout management with our Snap Layout feature, offering an exceptional user experience.

## Getting Started:

1. Clone the Urban UI repository.
2. Integrate the library into your WPF project.
3. Explore the enhanced controls and start creating a modern user interface.

## Usage:

### Modify `App.xaml`:

Open your `App.xaml` file and make the following modifications:

1. Add the Urban UI namespace at the top of the file:

    ```xml
    xmlns:urbanui="clr-namespace:UrbanUI.WPF.Controls;assembly=UrbanUI.WPF"
    ```

2. Include the Urban UI `ControlsDictionary` in the `MergedDictionaries` section:

    ```xml
    <ResourceDictionary.MergedDictionaries>
        <urbanui:ControlsDictionary/>
    </ResourceDictionary.MergedDictionaries>
    ```

These changes integrate Urban UI controls into your WPF application, providing a seamless modern look.

### Modify `MainWindow.xaml`:

Open your `MainWindow.xaml` file and make the following modifications:

1. Add the Urban UI namespace at the top of the file:

    ```xml
    xmlns:urbanUI="clr-namespace:UrbanUI.WPF.Controls;assembly=UrbanUI.WPF"
    ```

2. Replace your existing `Window` declaration with the Urban UI `Window`:

    ```xml
    <urbanUI:Window x:Class="MultiTool.MainWindow"
                 xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                 xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                 xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                 xmlns:urbanUI="clr-namespace:UrbanUI.WPF.Controls;assembly=UrbanUI.WPF"
                 Width="800"
                 Height="600"
                 MinWidth="650"
                 MinHeight="600"
                 Title="Multi-Tool"
                 mc:Ignorable="d" ResizeMode="CanResize">
    </urbanUI:Window>
    ```

These changes allow you to use the Urban UI `Window` in your MainWindow, providing a modernized and customizable window layout.

---

## Screenshots:

1. **Sample UI Application:**
   ![Sample UI](https://github.com/UrbanCastles/UrbanUI/blob/master/samples/test_app.png)

2. **Snap Layout Support (Feature Highlight):**
   ![Snap Layout](https://github.com/UrbanCastles/UrbanUI/blob/master/samples/snap_layout.png)

---

## Contribution:

We welcome contributions! Feel free to open issues, submit pull requests, or provide feedback to help us make Urban UI even better.

🚀 **Transform your WPF applications into modern masterpieces with Urban UI!** 🚀
