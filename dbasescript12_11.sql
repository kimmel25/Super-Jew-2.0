create table FollowedShuls
(
    FollowID int auto_increment
        primary key,
    UserID   int null,
    ShulID   int null
);

create table Gabbai
(
    GabbaiID int auto_increment
        primary key,
    UserID   int null,
    ShulID   int null,
    constraint Gabbai_pk
        unique (GabbaiID)
);

create table GabbaiShulRequests
(
    RequestID      int auto_increment
        primary key,
    RequestName    varchar(255) null,
    Location       varchar(255) null,
    Denomination   varchar(255) null,
    ContactInfo    text         null,
    ShachrisTime   varchar(255) null,
    MinchaTime     varchar(255) null,
    MaarivTime     varchar(255) null,
    GabbaiID       int          null,
    ApprovalStatus varchar(255) null,
    UserID         int          null
);

create index FK_GabbaiShulRequests_Gabbai
    on GabbaiShulRequests (GabbaiID);

create index FK_GabbaiShulRequests_Users
    on GabbaiShulRequests (UserID);

create table RabbAIInteractions
(
    InteractionID int auto_increment
        primary key,
    UserID        int      null,
    Timestamp     datetime null,
    UserTextEntry text     null,
    RabbAIAnswer  text     null
);

create table Shuls
(
    ShulID       int auto_increment
        primary key,
    ShulName     varchar(255) null,
    Location     varchar(255) null,
    Denomination varchar(255) null,
    ContactInfo  text         null,
    ShachrisTime varchar(255) null,
    MinchaTime   varchar(255) null,
    MaarivTime   varchar(255) null
);

create table ShulEvents
(
    EventID      int auto_increment
        primary key,
    ShulID       int          null,
    EventName    varchar(255) null,
    TimeOfEvent  varchar(255) null,
    Location     varchar(255) null,
    Subscription varchar(255) null,
    Description  varchar(255) null,
    constraint ShulEvents_Shuls_ShulID_fk
        foreign key (ShulID) references Shuls (ShulID)
);

create table Users
(
    UserID                int auto_increment
        primary key,
    Name                  varchar(255) null,
    Username              varchar(255) null,
    Password              varchar(255) null,
    Salt                  varchar(255) null,
    DateOfBirth           varchar(255) null,
    ReligiousDenomination varchar(255) null,
    AccountType           varchar(50)  null,
    check (`AccountType` in (_utf8mb4\'ADMIN\',_utf8mb4\'GABBAI\',_utf8mb4\'USER\'))
);

create table YidGits
(
    PreferenceID    int auto_increment
        primary key,
    UserID          int          null,
    SelectedWidgets varchar(255) null
);




