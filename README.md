# Graduation-Project
유니티 생존게임

## 기획의도
- 다수의 생존 게임들은 아무것도 없는 상황에서 생존을 시작한다.
- 실제 재난 상황에서는 미리 대비하는것이 중요하다.
- 게임 시작 전에 아이템을 미리 구매하는 요소를 차별화 포인트를 가지도록 하였다.

## 참고 게임
### 60Seconds!
- 핵폭발 전 60초 동안 아이템을 모아서 벙커에 들어가면 게임이 시작함.
  ![60초 준비](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/d4b7599b-6831-43eb-9c5e-85fb2a92988b)


## 구현결과

### 1. 시작/종료
![Start_1](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/584d0033-c111-4530-9b7b-29d140cf8f2f)

### 2. 구매 화면
![Start_Purchase](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/fd7ef115-6f9b-4a98-b664-e55bcde2ca8c)

### 3. 인벤토리
- 시작화면에서 구매한 아이템을 인벤토리에 저장

![Main_Inven](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/a0b1c003-6dfb-49a9-883c-1b7376ca7ed4)

### 4. 스테이터스
- 체력 / 허기 / 갈증 구현
- 허기와 갈증 수치는 주기적으로 감소
- 허기 및 갈증 수치가 0이 되면 체력이 감소
  
![Main_Status](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/660a0bb0-c726-4310-9209-0c192b276622)

### 5. 시간
- 침대를 바라보고 E키를 눌러 상호작용하면 하루가 지남
- 일정 기간 생존하면 성공

![Main_Day](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/719d52ce-1662-46e7-ae3e-f826edf74f8f)

![Main_Sleep](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/6d382ff6-6965-4fe6-a64c-b9579e8ed43d)

### 6. 맵 이동
- M키를 눌러 맵 화면 출력
- 남은 Active Point를 소모하여 이동 가능
- Active Point는 잠을 자면 회복

![Main_Map](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/6f62f6a5-9127-40f6-8322-d0043d25f60f)
### 7. 탐험
- 원하는 지역을 선택하여 탐험
  
![Main_Roof](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/ff41ce4c-a8d0-410b-ace1-0073725cc7de)

### 8. 아이템 획득
- 잔해 더미에서 아이템이 랜덤 생성
- E키를 눌러 상호작용
- 생성된 아이템을 우클릭하여 인벤토리에 저장

![Main_Farming](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/bef8cf72-d6b3-4951-a0e4-76f36647c8b5)

### 9. 전투
- 랜덤하게 생성된 적이 플레이어를 추격
- 인벤토리에서 무기 아이템을 우클릭하여 장착
- 적에게 좌클릭하여 공격
- 적에게 공격받으면 체력 감소
  
![Main_Attack](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/ba31f796-40d7-4de6-97bb-a807c43dc58b)

### 10. 엔딩

![End](https://github.com/JangSeonJong/Graduation-Project-2/assets/148080958/4de12199-4a2e-4b53-b95b-62a52aa7d571)

## 미구현된 기획
### 1. 방사능 수치
- 핵폭발 이후 반감기에 따라 최고 수치에서 2일후 10분의 1, 14일후 1000분의 1로 감소하는 방사능 수치
- 방사능 수치에 따라 탐험 시 체력이 지속적으로 감소
- 방독면 착용 시, 체력 대신 방독면 내구도 감소

### 2. 생존자 AI
- 적대/중립/우호 성향을 가지는 생존자 NPC 랜덤 생성
- 적대 성향 NPC는 플레이어를 발견하면 공격
- 중립 성향 NPC는 플레이어가 일정 범위보다 가까이 접근하면 공격
- 우호 성향 NPC는 플레이어가 먼저 공격하지 않으면 공격하지 않음

### 3. 생존자 NPC 및 자원 생성 알고리즘
- 생존자 NPC와 자원 생성은 생존 날짜에 따라 변화함
- 초기에는 살아남은 생존자와 약탈되지 않은 자원이 많으므로 생성량과 우호 성향 NPC가 많음.
- 시간이 지날수록 생존자와 자원 생성량이 감소하고, 적대 성향 NPC가 증가함.

### 4. 아이템 활용
- 아이템 구매 단계에서 다양한 아이템 구매 가능
- 덕테이프 : 핵폭발시 창문과 문틈을 막아 초기 피해 대응. 미구매 시, 체력이 감소한 상태로 게임 시작
- 방독면 : 탐험 시 방사능으로 인한 체력 감소를 막아줌. 방사능 수치에 따라 내구도 감소 수준이 달라짐.
- 손전등 : 어두운 공간 또는 밤 시간 동안 시야 감소를 막아줌.
