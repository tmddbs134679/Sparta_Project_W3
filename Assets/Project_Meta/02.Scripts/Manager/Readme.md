# 🧭 ObjectPool + Factory

- 몬스터 생성은 **ObjectPool**과 **Factory 패턴**을 결합한 **PoolFactory**로 최적화하였습니다.

---

## 📌 목차

- [PoolFactory](#PoolFactory)
- [MonsterFactoryManager(몬스터팩토리)](#MonsterFactoryManager)
- [ProjectileFactoryManager(투사체팩토리)](#ProjectileFactoryManager)

---

## 🎮 코드

### PoolFactory

| Script명                           | 설명                                                         |
| ---------------------------------- | ------------------------------------------------------------ |
| [PoolFactory.cs](./PoolFactory.cs) | Generic Pooling 시스템. 오브젝트를 미리 생성하고 관리합니다. |

### MonsterFactoryManager

| Script명                                              | 설명                                                         |
| ----------------------------------------------------- | ------------------------------------------------------------ |
| [MonsterFactoryManager.cs](/MonsterFactoryManager.cs) | 몬스터 생성 전용 Factory. 몬스터를 풀에서 가져오고, 반환합니다. |

### ProjectileFactoryManager

| Script명                                                    | 설명                                                         |
| ----------------------------------------------------------- | ------------------------------------------------------------ |
| [ProjectileFactoryManager.cs](/ProjectileFactoryManager.cs) | 투사체 생성 전용 Factory. 투사체를 풀에서 가져오고, 발사 및 반환합니다. |



---

[🔝 돌아가기](../../../../README.md)