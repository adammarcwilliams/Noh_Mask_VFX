# Japanese Noh Masks Interactive

> Noh (能 Nō), derived from the Sino-Japanese word for "skill" or "talent", is a major form of classical Japanese musical drama that has been performed since the 14th century... Noh is often based on tales from traditional literature with a supernatural being transformed into human form as a hero narrating a story. Noh integrates masks, costumes and various props in a dance-based performance, requiring highly trained actors and musicians. -- <cite>https://en.wikipedia.org/wiki/Noh</cite>

This touch screen interactive takes several common masks from traditional Japanese Noh theater and presents them in a stylised visual manor using Unity's new Visual Effect Graph to render them as point clouds with fluid transitions as you swipe between them.

Each mask is accompanied by a short audio clip describing the characteristics the mask commonly imbues during the play, from mischievous spirits to jealous demons.

## Project Details

* Unity: 2019.1.1f1
* LWRP: 5.13
* Visual Effect Graph: 5.13.0 - preview package
* Post Processing: 2.1.6
* Designed for 16:9 aspect ratio 1920 x 1080

You can clone this repo and run the project in your editor/build for supported platforms or if using a PC simply download the build below.

Visual Effects Graph requires a modern graphics card that supports compute shaders.

My test machines were:
* Window 10 PC with i7 CPU and NVIDIA GTX 1060 graphics
* 27" 5k iMac with i5 CPU and Radeon R9 M395 graphics

I used Unity's LWRP which has experimental support for the Visual Effects Graph in the latest version (unlit particles only) in the hopes that I could build the project to my iPhone XR.

After testing I found that at present, the performance was not good enough and I discovered a bunch of rendering glitches on iOS/Metal depending on the output nodes used in the graph.

This is something I plan to explore further with a VR port to my Oculus GO using OpenGL ES 3.1 to see if it is only an issue with Metal in the current preview package.

PC Build: 


## Technical Overview

The interactive revolves around a single VFX graph that contains a different node tree for each mask with a reference to it's points cache (pCache).

This is controlled via exposed parameters and events in the MasksController.

I used an asset pack of Japanese Noh masks and then converted them into pCache assets using Unity's Point Cache Baking Tool that is part of the VFX Graph package.

Input is handled by a simple script that works with mouse/single finger touch to detect a left/right swipe and fires off that GameEvent.

I used a Scriptable Object based event system inspired by this [Unite talk by Ryan Hipple](https://www.youtube.com/watch?v=raQ3iHhE_Kk), which allows events to be created and used like assets. You can then listen for them with a simple GameEventListener that exposes a Unity Event, making it easy to create decoupled code that doesn't rely on any singleton manager classes.

I also used Scriptable Objects for the Mask to keep the name string(required for the VFX Graph events) and audio clips packaged neatly inside an asset.


## Future Iterations

Part of the magic of Noh mask's are the different emotions they convey by tilting them at slightly different angles, so it would be good to take the interactive into an AR app where the mask tracks the users face position, allowing them to explore this in a fun way.

This is something I plan to do once the performance issue with iOS/Metal have been resolved.