# API-testanrop #
## 1. Hämta alla personer i systemet 

**Request:**
GET https://localhost:7001/api/User/GetAllUser

**Response:**
```Json
[
  {
    "id": 1,
    "fullName": "Anna Svensson",
    "email": "anna.svensson@gmail.com",
    "phone": "0701234567"
  }
]
```

##  2. Hämta alla intressen kopplade till en specifik person

**Request:** 
GET https://localhost:7001/api/User/GetInterestById/2

**Response:** 
```Json
{
  "id": 2,
  "fullName": "Erik Johansson",
  "interest": [
    {
      "interestId": 2,
      "title": "Träning",
      "description": "Fysisk aktivitet som gym, löpning eller sport"
    }
```

## 3. Hämta alla länkar kopplade till en specifik person

**Request:**
GET https://localhost:7001/api/User/GetLinkById/3

**Response:**
```Json
{
  "id": 3,
  "fullName": "Sara Andersson",
  "link": [
    {
      "id": 5,
      "url": "https://tripadvisor.com"
    }]
}
```

## 4.Koppla en person till ett nytt intresse

**Request:**
POST https://localhost:7001/api/User/AddInterestInUser/5?interestId=1

**Response:**
```Json
{
  "id": 18,
  "url": null,
  "userId": 5,
  "interestId": 1,
  "interest": {
    "id": 1,
    "title": "Programmering",
    "description": "Att skriva kod och utveckla applikationer"
  }
}
```

## 5.Lägga till nya länkar för en specifik person och ett specifikt intresse

**Request:**
POST https://localhost:7001/api/User/AddNewLink?userId=5&interestId=1&url=https%3A%2F%2Fstackoverflow.com%2Fquestions

**Response:**
```Json
{
  "id": 18,
  "url": "https://stackoverflow.com/questions",
  "userId": 5,
  "interestId": 1,
  "interest": {
    "id": 1,
    "title": "Programmering",
    "description": "Att skriva kod och utveckla applikationer"
  }
}
```

