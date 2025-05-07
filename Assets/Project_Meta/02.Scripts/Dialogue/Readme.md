# 🧭 NPC 대화 시스템 (Dialogue System)

NPC와 플레이어 사이의 대화를 관리하고 출력하는 시스템입니다.

플레이어는 NPC와 상호작용하며 대사를 확인하고, 선택지를 통해 대화의 흐름을 바꿀 수 있습니다.
CSV 기반으로 작성된 대사 데이터를 불러와서 NPC의 상태나 선택지에 따라 동적인 대화 시퀀스를 제공합니다.

---

## 📌 목차

- [DialogueModel](#DialogueModel)
- [DialogueController](#DialogueController)
- [DialogueUI](#DialogueUI)

---

## 🎮 코드

### DialogueModel

| Script명                             | 설명                                        |
| ------------------------------------ | ------------------------------------------- |
| [DialogueModel.cs](DialogueModel.cs) | CSV 기반 대사 데이터를 로드하고 관리합니다. |

### DialogueController

| Script명                                   | 설명                                               |
| ------------------------------------------ | -------------------------------------------------- |
| [DialogueController.cs](DialogueController.cs) | 대화의 흐름을 제어하고 플레이어 입력을 처리합니다. |

### DialogueUI

| Script명                             | 설명                                           |
| ------------------------------------ | ---------------------------------------------- |
| [DialogueUI.cs](../UI/DialogueUI.cs) | NPC 이름, 대사, 선택지 등을 화면에 표시합니다. |



---

[🔝 돌아가기](../../../../README.md)
