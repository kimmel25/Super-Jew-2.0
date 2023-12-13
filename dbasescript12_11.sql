CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    Username VARCHAR(255),
    Password VARCHAR(255),
    DateOfBirth DATE,
    ReligiousDenomination VARCHAR(255),
    AccountType VARCHAR(50) CHECK (AccountType IN ('ADMIN', 'GABBAI', 'USER'))
);

CREATE TABLE Shuls (
    ShulID INT PRIMARY KEY,
    Name VARCHAR(255),
    Location VARCHAR(255),
    Denomination VARCHAR(255),
    ContactInfo TEXT,
    ShachrisTime VARCHAR(255),
	MinchaTime VARCHAR(255),
	MaarivTime VARCHAR(255),
    EventID INT 
);

CREATE TABLE FollowedShuls (
    FollowID INT PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    ShulID INT REFERENCES Shuls(ShulID)
);

CREATE TABLE ShulEvents (
    EventID INT PRIMARY KEY,
    ShulID INT REFERENCES Shuls(ShulID),
    EventName TEXT,
    TimeOfEvent DATETIME,
    Location TEXT,
    Speaker TEXT
);

CREATE TABLE Gabbai (
    GabbaiID INT PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    ShulID INT REFERENCES Shuls(ShulID),
    GabbaiName TEXT
);

CREATE TABLE RabbAIInteractions (
    InteractionID INT PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    Timestamp DATETIME,
    UserTextEntry TEXT,
    RabbAIAnswer TEXT
);

CREATE TABLE YidGits (
    PreferenceID INT PRIMARY KEY,
    UserID INT REFERENCES Users(UserID),
    SelectedWidgets VARCHAR(255)
);

CREATE TABLE PendingShulsData(
    ShulDataApprovalID INT PRIMARY KEY,
    ShulID INT REFERENCES Shuls(ShulID),
    Name VARCHAR(255),
    Location VARCHAR(255),
    Denomination VARCHAR(255),
    ContactInfo TEXT,
    ShachrisTime DATETIME,
	MinchaTime DATETIME,
	MaarivTime DATETIME,
    EventID INT
    );

CREATE TABLE GabbaiRequests(
    RequestID INT PRIMARY KEY,
    GabbaiID INT REFERENCES Gabbai(GabbaiID),
    ApprovalStatus VARCHAR(255),
    ShulDataApprovalID INT references PendingShulsData(ShulDataApprovalID)
);

ALTER TABLE Shuls
ADD FOREIGN KEY (EventID) REFERENCES ShulEvents(EventID);
