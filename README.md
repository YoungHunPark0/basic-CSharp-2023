# basic-CSharp-2023
부경대 2023  객체지향(C#) 프로그래밍 리포지토리

 <!-- 
 https://drive.google.com/drive/folders/1TDrL5UO_dbADhBb_T7Ah6URNluIUwY4P
 깃허브 리포지토리 - public, readme.md 체크, gitignore- visual stdio 체크
=======
visual studio install ->  수정-> 워크로드 asp.net, .net데스크톱개발 체크
-> 개별 구성요소 net3.1 체크 추가

SQL2022-SSEI-Dev.exe - 미디어다운로드- iso 다운로드
- iso파일 마우스오른쪽버튼- 탑재->setup.exe->왼쪽항목 설치
->새 sql설치기능추가->개발자->동의->체크안하고다음->방화벽경고는 외부에서 나중에 풀어야함
->Azure확인 체크해제->기능 db엔진서비스, 서버복제, 검색을 위한, data Quality 체크
인스턴스루트 - C:\DEV\Server\Microsoft SQL Server\로 변경(dev에 server폴더 만들기)
계속다음-> 서버구성 혼합모드체크, 암호12345입력->현재사용자추가->설치

https://learn.microsoft.com/ko-kr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
-> 내려서 한국어 다운로드->SSMS-Setup-KOR.exe->
경로C:\DEV\Tools\Microsoft SQL Server Management Studio 19

visual stdio 새프로젝트 - c#, windows, 콘솔 체크-> 2개나옴
콘솔앱 .net framework 이전버전, 그냥 콘솔앱 최신

도구-설정가져오기 및 내보내기-파일경로 변경-저장

c#으로 데이터베이스 연동하기
SSMS sql서버 - 예시파일 https://github.com/microsoft/sql-server-samples
다운받고 압출풀기 
환경 - 글꼴 및 색 - 글꼴변경
모든언어 - 줄번호 체크
sqlserver 개체 탐색기 테이블 및 뷰 옵션 - 상위 <n>개 둘다 0
압축푼 파일 중 instnwnd.sql, instpubs.sql파일 마우스 드래그해서
ssms에 끌어서 넣기 
- 실행 하면 왼쪽 테이블에 새로고침하면 추가됨

https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=lwj798&logNo=220787369857
c#으로 데이터베이스 연동하기
visualstdio - 보기 - 서버탐색기 - 연결추가 - ms sql server체크 (항상밑에 체크)
서버이름 localhost, 인증-sql server인증- id sa // pw 12345
데이터베이스 이름선택 - Northwind 선택 (연결테스트 통과하기)
고급 - 맨밑에 Data Source=localhost;Initial Catalog=Northwind;User ID=sa 복사
확인 - 확인 - 연동해놓은 FrmMain cs파일에 코드보기(f7)
connection string = 데이터 문자열
string connstring = "Data Source=localhost;Initial Catalog = Northwind; User ID = sa;Password=12345;";
복사한거 붙여넣기하기! 패스워드는 나중에 따로 입력
DB핸들링
https://docs.google.com/document/d/1jUSYBeAwMinuZqWqamPEyr_tj1fGjugkKU3G1FGnte0/edit#heading=h.8a9sxshwch3f
도구상자 데이터 - 데이터그리드뷰 넣고 - 오른쪽상단 삼각형 - 프로젝트데이터추가
데이터 더블클릭 - 예 - 보이기 다음다음 - 테이블 employ만 체크 - 실행하면 자동으로 뜸
 -->

## 1일차
- C# 기본
	- .NET framework / .NET 5.0 이후
	- Visual studio상 C# 구성
	- 기본 문법
	
## 2일차
- C# 기본
	- 기본 문법(변수, 메서드, 연산자, 제어(if, for문))
	
- Win App
	- WinForms vs WPF 개요
	- WinForms 기초
	
## 3일차
- C# 기본
	- 클래스

- Win App
	- WinForms 컨트롤
	- 리스트뷰, 데이터그리드 추가학습

3일차 윈폼 학습결과

<img
src="https://raw.githubusercontent.com/YoungHunPark0/basic-CSharp-2023/main/images/winform.png" width="700">


## 4일차
- C# 기본
	- 클래스 상속(Inheritance) 계속
	- 인터페이스

- Win App
	- WinForms 컨트롤 마무리
	- WinForms 파일복사 앱
	- 로그인앱 실습
	
## 5일차
- C# 기본
	- 인터페이스, 추상클래스
	- 프로퍼티
	- 배열, 컬렉션
	
- Win App
	- WinForms 디자인 오류시 해결방법
	- Window 탐색기 만들기
	
5일차 윈도우 탐색기 만들기 중

<img
src="https://raw.githubusercontent.com/YoungHunPark0/basic-CSharp-2023/main/images/winform2.png" width="700">

## 6일차
- C# 기본
	- 컬렉션
	- 일반화 프로그래밍
	- 예외처리
	- 대리자, 이벤트
	
- Win App
	- 탐색기 마무리
		- 추가개발 리스트
		- 컨텍스트 메뉴(마우스 오른쪽 메뉴)
		- 보호된 운영체제 폴더 숨기기
		- 리스트뷰 폴더 더블클릭시 하위폴더 표시
	- DB핸들링

6일차 윈도우 탐색기 만들기 완료

<img
src="https://raw.githubusercontent.com/YoungHunPark0/basic-CSharp-2023/main/images/winform3.png" width="700">

## 7일차
- C# 기본
	- 대리자(콜백 - 이벤트등 대신해주는 것), 이벤트
	- 람다식
	- 애트리뷰트
	- 더 공부해야할 내용(LINQ, 리플렉션, dynamic)
	
- Win App
	- DB핸들링
	- SDI vs MDI
	
## 8일차
- C# 기본
	- 파일핸들링
	- 윈폼 메모장(파일핸들링)
	
- Win App
		- BookRentalShop DB 사용 WinForms 앱개발
			- WinForms MDI방식으로 개발
			- MySQL.Data Lib DB연동
	
심플 메모장

<img
src="https://raw.githubusercontent.com/YoungHunPark0/basic-CSharp-2023/main/images/winform4.png" width="700">

## 9일차
- C# 기본
	- 스레드 / 태스크
	- 가비지 컬렉션
	
- Win App
	- BookRentalShop 만들기 계속
	
## 10일차
- Win App
	- BookRentalShop 만들기 마무리
	
- 코딩테스트
<!-- 
https://docs.google.com/forms/d/e/1FAIpQLSdaYqL1wO6Wx3Y1IXXhy3tFQMvJ7A1E3D3Tl5Ga_vB3jmzF0w/viewform -->

10일차 책대여점 프로그램
<img
src="https://raw.githubusercontent.com/YoungHunPark0/basic-CSharp-2023/main/images/winform5.png" width="700">