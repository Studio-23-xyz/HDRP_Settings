<h1 align="center">Settings Manager</h1>
<p align="center">
<a href="https://openupm.com/packages/com.studio23.ss2.settingsmanager/"><img src="https://img.shields.io/npm/v/com.studio23.ss2.settingsmanager?label=openupm&amp;registry_uri=https://package.openupm.com" /></a>
</p>

This package allows you to easily place UI elements linked to backend to give users the ability to change various settings during runtime. You can control various Video and settings through this package.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
   - [Using Settings Manager](#Using-Settings-Manager)

## Installation

### Install via Git URL

You can also use the "Install from Git URL" option from Unity Package Manager to install the package.
```
https://github.com/Studio-23-xyz/HDRP_Settings.git#upm
```

## Usage

### Using Settings Manager

This packages native UI elements along with Text Mesh Pro to make up the settings UI. In order to get it working,

1. Find the SettingsManager_Parent prefab under Prefab folder. 

2. Drag to your scene of choice.

3. The prefab already contains a Canvas as a child. You will have to manually select the Audio Mixer groups that will correspond to the sliders.

4. You can find the AudioSettingsController script attached to the GameObject named, "1 AudioSettingsController". 

5. Expand the Audio Settings section and assign the MixerGroup along with label name and the exposed parameter that controls the volume of the Mixer.

6. Hit the play button and you're all good to go. 