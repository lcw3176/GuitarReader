# GuitarReader
#### 본 프로젝트는 [GuitarReader_Atmega2560](https://github.com/lcw3176/GuitarReader_Atmega2560) 과 연동된 프로젝트 입니다.
## 개요
### 소개
- 사운드 센서로 입력된 기타 소리를 분석해서 악보로 변환해 주는 프로그램입니다.
- 녹음한 정보를 DB에 저장 후, 재생하여 들어 볼 수 있습니다.
### 기능
- 악보 저장
- 간단한 편집
- 저장된 악보 재생
### 기술 스택
- C#, WPF
- Visual Studio 2019
- .Net Framework 4.7.2
- MaterialDesignThemes
- SQLite
### 참조 DLL
- [Toub.Sound.Midi](http://grouplab.cpsc.ucalgary.ca/cookbook/index.php/VisualStudio/HowToPlayMIDIInstruments)

## 작동 모습
## 메인 홈
![홈](https://user-images.githubusercontent.com/59993347/143394900-1d19c2dd-18c4-45c6-95ba-e69361aee437.png)
- 프로젝트의 간단한 소개글과 시리얼 연결 기능이 존재합니다.
- bps는 9600과 115200 두 개가 존재합니다.

## 분석
![분석](https://user-images.githubusercontent.com/59993347/143394910-75eaac02-3f90-4bda-b6be-d6d319d3bd0a.png)
- 현재 자신이 짚은 위치가 올바르게 표기되고 있는지 볼 수 있습니다.
- 실제 연주 시, 그에 맞는 코드가 동적으로 표기됩니다.

## 녹음
![녹음](https://user-images.githubusercontent.com/59993347/143394786-2fb0deed-3f96-4ac4-a7d3-2c2eb5ba67af.png)
- 우측의 동그란 버튼을 눌러서 녹음을 시작, 중지 할 수 있습니다.
- 디스켓 모양의 버튼을 눌러 저장이 가능합니다.

## 악보 목록
![악보목록](https://user-images.githubusercontent.com/59993347/143394790-5454f69f-6355-4abb-bf5b-9a002dc57644.png)
- 자신이 녹음해놓은 악보를 볼 수 있습니다.
- 악보를 들어보거나 간단한 편집이 가능하며, 날짜들이 기록되어 있습니다.

## 재생
![재생](https://user-images.githubusercontent.com/59993347/143394795-6f9aa97a-5eb9-40a3-bbe3-75df7a5ed141.png)
- 기록된 기타 프렛의 위치에 맞게 Toub.Sound.Midi를 활용, 재생합니다.
- 재생, 일시정지, 정지 기능이 존재합니다.

## 편집
![편집](https://user-images.githubusercontent.com/59993347/143394797-8c4eac03-94d6-488e-b7a1-882ff18a1e9e.png)
- 기록된 악보를 편집할 수 있습니다.
- 우측 상단의 버튼 두 개로 악보의 시작, 끝으로 이동이 가능합니다.
- 숫자로 기록된 음표를 클릭하는 방식으로 편집합니다.
    - 우클릭: 1초 뒤로 이동
    - 좌클릭: 1초 앞으로 이동
