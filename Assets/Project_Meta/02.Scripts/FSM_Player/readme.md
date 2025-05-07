# 🧭 Player FSM

FSM 패턴 기반의 Player 상태를 구현한 코드입니다.

플레이어의 이동, 대기, 공격 등 다양한 행동을 상태 기반으로 관리하는 구조입니다.  
상태 전환 로직을 명확히 하여 유지보수성과 확장성을 높였습니다.

---

## 📌 목차

- [PlayerStateMachine (상태 매니저)](#playerstatemachine)
- [PlayerIdleState (대기 상태)](#playeridlestate)
- [PlayerMoveState (이동 상태)](#playermovestate)
- [PlayerAttackState (공격 상태)](#playerattackstate)

---

## 🎮 코드

### PlayerStateMachine

| Script명 | 설명 |
|----------|-----|
| [PlayerStateMachine.cs](./Project_Meta/02.Scripts/FSM_Player/StateMachine/PlayerStateMachine.cs) | FSM 메인 매니저. 상태 전환 및 현재 상태 갱신 처리 |

### PlayerIdleState

| Script명 | 설명 |
|----------|-----|
| [PlayerIdleState.cs](./Project_Meta/02.Scripts/FSM_Player/State/PlayerState/PlayerIdleState.cs) | 플레이어의 대기 상태를 정의한 클래스 |

### PlayerMoveState

| Script명 | 설명 |
|----------|-----|
| [PlayerMoveState.cs](./Project_Meta/02.Scripts/FSM_Player/State/PlayerState/PlayerMoveState.cs) | 플레이어의 이동 상태를 정의한 클래스 |

### PlayerAttackState

| Script명 | 설명 |
|----------|-----|
| [PlayerAttackState.cs](./Project_Meta/02.Scripts/FSM_Player/State/PlayerState/PlayerAttackState.cs)  | 플레이어의 공격 상태를 정의한 클래스 |

---

[🔝 돌아가기](#목차)
