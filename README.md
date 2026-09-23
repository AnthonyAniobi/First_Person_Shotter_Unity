# First Person Shooter in Unity

A Unity first-person shooter prototype built while exploring player movement, camera control, the Input System, and simple projectile weapons.

## Current Features

- First-person player movement using a `CharacterController`.
- Walking with keyboard, gamepad, or other supported Input System devices.
- Mouse-look camera rotation with vertical look clamping.
- Jumping with configurable jump force and gravity.
- Projectile firing using a reusable bullet prefab and Rigidbody physics.
- Automatic bullet cleanup after a configurable lifetime.
- Target collision handling using the `Target` tag.
- Universal Render Pipeline (URP) project configuration.

This is an in-progress prototype rather than a complete game. Enemy behavior, health, scoring, UI, audio, levels, and multiplayer systems have not been implemented yet.

## Requirements

- Unity `6000.4.2f1` or a compatible Unity 6 installation.
- A keyboard and mouse, or a supported gamepad.

## Getting Started

1. Open the project in Unity Hub using the Unity version above.
2. Open `Assets/Scenes/SampleScene.unity`.
3. Select the player object and confirm that its `CharacterController`, `PlayerMovement`, and `MouseMovement` components are configured.
4. Confirm that the weapon has a bullet prefab and bullet spawn point assigned.
5. Press Play to test movement, looking, jumping, and firing.

`SampleScene.unity` is also the scene currently enabled in the project's build settings.

## Controls

| Action | Keyboard and mouse | Gamepad |
| --- | --- | --- |
| Move | `WASD` or arrow keys | Left stick |
| Look | Mouse movement | Right stick |
| Jump | Space | South button, such as A/Cross |
| Fire | Left mouse button | West button, such as X/Square |
| Previous | `1` | D-pad left |
| Next | `2` | D-pad right |
| Sprint | Left Shift | Left stick press |

Movement, look, jump, attack, and the other actions are defined in `Assets/InputSystem_Actions.inputactions`.

## Project Structure

### `Assets/Scripts`

- `PlayerMovement.cs` reads the `Move` and `Jump` actions, converts input into world movement, applies gravity, and moves the player through a `CharacterController`.
- `MouseMovement.cs` reads the `Look` action, locks the cursor, and rotates the player or camera while clamping vertical rotation.
- `Weapon.cs` reads the `Attack` action, instantiates a bullet at the configured spawn point, applies forward impulse force, and destroys the bullet after its lifetime expires.
- `Bullet.cs` listens for collisions. When a collision is with an object tagged `Target`, it logs the hit and destroys the bullet.

### `Assets/Scenes`

- `SampleScene.unity` is the playable prototype scene and the only scene currently included in the build settings.

### `Assets/Prefabs`

- `Bullet.prefab` is the projectile prefab instantiated by `Weapon.cs`. It should include the components required for physics and collision detection, including a Rigidbody and collider.

### `Assets/Materials`

- Contains the current red, green, and blue materials used by scene or prefab objects.

### `Assets/Settings`

- Contains Unity project and tutorial-related settings assets.

## Packages and Configuration

- Unity Input System `1.19.0` provides action-based input.
- Universal Render Pipeline `17.4.0` provides the rendering pipeline.
- AI Navigation, Timeline, UI, Visual Scripting, and Unity Test Framework packages are installed for future development or experimentation.

## Current Progress

The current build demonstrates the core FPS loop: move around the scene, look with the mouse, jump, and fire projectiles. The project is ready for the next gameplay layer, such as targets with health, enemy AI, weapon feedback, level design, and a user interface.

<img src="/screenshots/current_stage.gif" width="240" alt="Current project progress">
