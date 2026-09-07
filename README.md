# Nexon-Tutorial

생성형 AI 도구로 익숙한 기능을 빠르게 구현한 **2D 서바이벌 웨이브 슈터** (Unity 6, C#)

## 목적

이미 익힌 유니티 기능(입력, 물리, 프리팹, 이벤트)을 **생성형 AI 도구를 활용해 빠르게 조립**하는 연습.
같은 게임을 데이터 주도 설계·세이브 시스템으로 재구성한 버전 → **[Nexon-Tutorial2-Data](https://github.com/kv0630028/Nexon-Tutorial2-Data)**

## 플레이

| 입력 | 동작 |
|---|---|
| WASD | 이동 |
| 마우스 | 조준 |
| 공격 입력 | 총알 발사 |
| R (사망 시) | 재시작 |

사방에서 스폰되는 적을 처치. 웨이브를 클리어할 때마다 적이 1마리씩 늘고, 피격으로 HP가 0이 되면 게임 오버.

## 구현

- **플레이어** — 이동, 마우스 방향 조준, 총알 발사(수명·피격 처리)
- **적** — 플레이어 추적, 근접 공격(쿨다운), 체력·사망
- **웨이브** — 링 형태 스폰, 전멸 시 다음 웨이브 자동 시작
- **점수 / UI** — 처치 점수, HP·점수·게임오버 표시, R 재시작
- **이벤트 기반 구조** — `PlayerHealth.OnHealthChanged / OnDeath`, `EnemyHealth.OnDeath`, `ScoreManager.OnScoreChanged` 를 UI가 구독 (폴링 없음)

## 구조

```
Assets/Script/
├─ Player/   PlayerMovement · PlayerAttack · PlayerBullet · PlayerHealth
├─ Enemy/    EnemyMovement · EnemyAttack · EnemyHealth
├─ Wave/     WaveManager · GameOverUI · PlayerHealthUI · RestartManager
└─ Score/    ScoreManager · ScoreUI
```

## 환경

Unity 6 · C# · Input System · TextMeshPro
