# Archero-Clone Demo
<div align="center">
  <a href="https://batuhankanbur.itch.io/archero-clone" target="_blank">
	<img src="https://s14.gifyu.com/images/bxxiB.gif" alt="Game Preview" width="200">
  </a>
</div>
<p align="center">
  <a href="https://batuhankanbur.itch.io/archero-clone" target="_blank">
    <img src="https://s6.gifyu.com/images/bzzlE.png" alt="Play Button" width="150">
  </a>
</p>

A simple prototype replicating **Archero-style control and combat mechanics** using Unity. This project demonstrates modular design, physics-based projectile motion, and an extendable skill system with scalable architecture.

---

## 📖 Overview

This game is a **combat simulation** where the player controls an **archer character** via an on-screen joystick. When the character moves, attacks stop. Once the player stops moving, the character automatically targets and attacks the **nearest enemy**.

Enemies are **stationary dummies** that respawn after defeat, and the system features a **skill-based combat enhancement** system.

---

## 🎮 Mechanics

### 1. Control Mechanics (Joystick Movement)
- The character is controlled using an on-screen joystick.
- While moving, the character does not attack.
- Once the joystick is released, the character becomes stationary and starts attacking the nearest target automatically.

### 2. Combat Mechanics
- Attacks are triggered only when stationary.
- Arrows are fired using physics-based projectile motion (including gravity).
- The system calculates the proper trajectory for realistic behavior.

### 3. Enemies
- Enemies are **stationary cubes ("dummies")** with health bars.
- Once defeated, they **respawn at a random position** within the map.
- At least **5 enemies** are always visible on-screen.

### 4. Camera & Map
- Fixed camera in **16:9 portrait mode**.
- The entire map is visible without any camera movement.

---

## 🧠 Skill System

Five toggleable skills that instantly apply or remove effects:

1. **Arrow Multiplication**: Fires 2 arrows instead of 1.
2. **Bounce Damage**: Arrows bounce to the nearest enemy after hitting.
3. **Burn Damage**: Inflicts damage-over-time (DoT) for 3 seconds; stackable.
4. **Attack Speed Increase**: Doubles the character's attack rate.
5. **Rage Mode**: Doubles the effect of all active skills:
   - Skill 1: Fires 4 arrows.
   - Skill 2: Bounces to two enemies.
   - Skill 3: Burn lasts 6 seconds.
   - Skill 4: Attack speed x4.

### 🔧 UI & Skill Activation
- Skills are shown in a **tab menu** that can be opened or closed.
- Each skill is a **toggle button**; active skills are visually indicated.
- Re-pressing the skill button **immediately deactivates** the effect.

---

## 💡 Key Technical Highlights

- ✅ **Object Pooling**: Efficient reuse of arrows and enemies.
- ✅ **Physics-Based Trajectories**: Realistic parabolic shooting.
- ✅ **Efficient Targeting**: Nearest enemy is detected using an optimized algorithm.
- ✅ **AimConstraint**: Used to enhance realism in aiming logic.
- ✅ **UniTask**: All asynchronous operations are handled using the UniTask library.
- ✅ **Event-Driven Architecture**: Avoids `Update()` by favoring events.
- ✅ **Animator StateMachineBehaviours**: Animation event system implemented via state machine behaviors.
- ✅ **Simple Service Locator**: Minimal but effective dependency resolver.
- ✅ **ScriptableObject Operator Overloading**: Combines Scriptables with `+` operator for modular data design.

---

## 🧩 Design Patterns Used

At least three major design patterns were implemented:

- **Strategy Pattern**: Used for modular attack behaviors.
- **Factory Method Pattern**: For spawning and configuring characters and arrows.
- **Observer Pattern**: For UI and game state updates.

---

## ⚙️ Additional Notes

- If you'd like to **replace dummy enemies with attacker mobs**, modify the `Level_01` ScriptableObject and replace the dummy references with `Attacker` types.
- The **Character class is abstract**, allowing it to be shared between `Player` and `Enemy` implementations.
- The **entire character behavior** is handled by a single `MonoBehaviour` script.

## 🚀 Getting Started

1. Open the project in Unity (2021.3+ recommended).
2. Load the `Level_01` scene.
3. Enter Play Mode and use the joystick to control the character.
4. Toggle skills via the skill menu.

---

## 📃 License

This project is released under the [MIT License](LICENSE).

---

## ✍️ Author

Developed by Batuhan Kanbur. Contributions, feedback, and forks are welcome!