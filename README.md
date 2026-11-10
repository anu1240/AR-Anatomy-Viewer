# 🫀 AR Anatomy Viewer

![Unity](https://img.shields.io/badge/Unity-2019.4.41f2-blue?logo=unity)
![Vuforia](https://img.shields.io/badge/Vuforia-8.1.12-green)
![Platform](https://img.shields.io/badge/Platform-Android-3DDC84?logo=android)

A prototype for viewing the internal parts of the human body using marker-based Augmented Reality.
The 3D models were made in Blender, exported as `.fbx`, and imported into Unity.

The app has three parts — **skeleton**, **brain** and **heart** — which can be grabbed, zoomed and rotated automatically.

- 📱 App: https://bit.ly/46msdX3
- 🎥 Demo video: https://bit.ly/4aGlCsV

## Getting started

1. Clone the repo and open the `Anatomy_of_Human_Body_using_AR` folder in **Unity 2019.4.41f2** (via Unity Hub).
2. Add your own Vuforia license key (free from the [Vuforia Developer Portal](https://developer.vuforia.com/)):
   `Assets/Resources/VuforiaConfiguration.asset` → `vuforiaLicenseKey`, or in Unity via **Window → Vuforia Configuration**.
   The key is intentionally left blank in this repo.
3. Switch the build target to **Android** and build.

## Project structure

```
Anatomy_of_Human_Body_using_AR/
├── Assets/
│   ├── Scenes/            # OpenScene, HumanBody, Skeleton, Skull, Brain, Heart
│   ├── Scripts/           # Grab, scale, rotate and label interactions
│   ├── models/            # Body, skeleton, skull, brain and heart models
│   ├── Materials/
│   └── StreamingAssets/   # Vuforia image target database
├── Packages/
└── ProjectSettings/
```
