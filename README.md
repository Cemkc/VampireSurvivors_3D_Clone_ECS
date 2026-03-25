# 🦇 Vampire Survivors 3D Clone (Unity ECS)

[![Vampire Survivors ECS Showcase](https://img.youtube.com/vi/fmzjpi9xANs/maxresdefault.jpg)](https://www.youtube.com/watch?v=fmzjpi9xANs)
*Click the image above to watch the full technical breakdown and gameplay showcase.*

## 📌 Project Overview
This project is a high-density, 3D top-down survival game inspired by the mechanics of *Vampire Survivors*. Originally assigned as a standard C# curriculum project, I deliberately elected to architect the entire game from scratch utilizing **Unity's Entity Component System (ECS)** and the **Data-Oriented Technology Stack (DOTS)**. 

The primary objective was to push beyond traditional Object-Oriented Programming (OOP) and master Data-Oriented Programming (DOP). By leveraging ECS, the game effortlessly handles massive, scaling entity counts (enemies, projectiles, and collectables) in a 3D space without compromising frame rate or processing overhead.

## 🛠️ Tech Stack
* **Engine:** Unity 3D
* **Language:** C#
* **Architecture:** Unity ECS (Entity Component System) / DOTS
* **Paradigm:** Data-Oriented Programming (DOP)

## 🚀 Technical Highlights & ECS Implementation

### Data-Oriented Programming (DOP) over OOP
In traditional Unity development, attaching `MonoBehaviour` scripts to thousands of GameObjects leads to severe memory fragmentation and CPU bottlenecking. This project circumvents that by relying entirely on raw data arrays and decoupled logic:
* **Entities:** Used strictly as lightweight memory IDs representing enemies, the player, and projectiles.
* **Components:** Implemented as `IComponentData` structs to ensure contiguous memory packing and optimal cache locality.
* **Systems:** Game logic (movement, collision, health) is processed via `ISystem` and the C# Job System, allowing heavy calculations to be distributed across multiple CPU cores in parallel.

### Massive Entity Management & Optimization
In the "horde survival" genre, performance is a core mechanic. Handling massive entity counts with 3D transforms and rendering is highly computationally expensive.
* **Cache Locality:** By organizing data sequentially in memory, CPU cache misses are drastically reduced. The engine can iterate over thousands of 3D enemy transforms and behaviors in a fraction of a millisecond.
* **Burst Compiler:** Utilized Unity's Burst Compiler to translate C# Jobs into highly optimized native code, maximizing the efficiency of mathematical operations like distance checks, vector math, and collision sweeps.

### Modular Systems Architecture
The game's logic is broken down into highly scalable, decoupled systems:
* **Spawning System:** Handles the dynamic generation of enemy entities without the overhead of traditional GameObject instantiation.
* **Movement & Swarm Systems:** Efficiently calculates trajectory and swarming logic toward the player without relying on expensive, individual NavMesh calculations for every unit.
* **Combat & Damage Pipeline:** Separates collision detection from health management, allowing sweeping attacks to instantly and cleanly parse damage across hundreds of targets at once.

## 💡 Why I Built This with ECS
When tasked with building a standard C# assignment, I realized that the horde survival genre is the ultimate testing ground for data-driven pipelines. I chose to go beyond the baseline requirements and self-teach Unity ECS because I wanted to understand how high-level studios handle massive scale and strict performance constraints. 

This project serves as a practical demonstration of my ability to foresee performance bottlenecks in a 3D environment and proactively adopt advanced architectural patterns to engineer a robust, scalable solution.

## 🔗 Connect
* **LinkedIn:** [Cem Koç](https://www.linkedin.com/in/cem-ko%C3%A7/)
