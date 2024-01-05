create
    definer = dbmasteruser@`%` procedure AddGabbaiToShul(IN InputUserId varchar(255), IN InputShulId varchar(255))
BEGIN
    INSERT INTO Gabbai (UserID, ShulID)
    VALUES (InputUserId, InputShulId);
END;

create
    definer = dbmasteruser@`%` procedure AddShul(IN inputShulID int, IN inputName varchar(255),
                                                 IN inputLocation varchar(255), IN inputDenomination varchar(255),
                                                 IN inputContactInfo text, IN inputShachrisTime varchar(255),
                                                 IN inputMinchaTime varchar(255), IN inputMaarivTime varchar(255))
BEGIN
    INSERT INTO Shuls (ShulID, ShulName, Location, Denomination, ContactInfo, ShachrisTime, MinchaTime, MaarivTime)
    VALUES (inputShulId, inputName, inputLocation, inputDenomination, inputContactInfo, inputShachrisTime, inputMinchaTime, inputMaarivTime);
END;



create
    definer = dbmasteruser@`%` procedure AddShulToUser(IN inputUserId int, IN inputShulId int)
BEGIN
    INSERT INTO FollowedShuls (UserID, ShulID)
    VALUES (inputUserId, inputShulId);
END;



create
    definer = dbmasteruser@`%` procedure ClearGabbaiAddedShul(IN inputRequestID int)
BEGIN
    DELETE FROM GabbaiShulRequests
    WHERE RequestID = inputRequestID;
END;



create
    definer = dbmasteruser@`%` procedure CreateEvent(IN inputShulID int, IN inputEventName varchar(255),
                                                     IN inputTimeOfEvent varchar(255), IN inputLocation varchar(255),
                                                     IN inputSubscription varchar(255),
                                                     IN inputDescription varchar(255))
BEGIN
    INSERT INTO ShulEvents (ShulID, EventName, TimeOfEvent, Location, Subscription, Description)
    VALUES (inputShulId, inputEventName, inputTimeOfEvent , inputLocation, inputSubscription, inputDescription);
END;



create
    definer = dbmasteruser@`%` procedure CreateNewUser(IN InputName varchar(255), IN InputUserName varchar(255),
                                                       IN InputPassword varchar(255), IN _salt varchar(255),
                                                       IN InputDateOfBirth date,
                                                       IN InputReligiousDenomination varchar(255),
                                                       IN InputAccountType varchar(255))
BEGIN
    -- Insert the new user into the Users table
    INSERT INTO Users  (Name, Username, Password, Salt, DateOfBirth, ReligiousDenomination, AccountType)
    VALUES (InputName, InputUserName, InputPassword, _salt, InputDateOfBirth, InputReligiousDenomination, InputAccountType);
END;



create
    definer = dbmasteruser@`%` procedure DeleteEvent(IN InputEventId int)
BEGIN
    DELETE from ShulEvents
    WHERE EventID = InputEventId;
end;



create
    definer = dbmasteruser@`%` procedure GetAllGabbais()
BEGIN

    SELECT
        u.UserID,
        u.Username,
        u.DateOfBirth,
        u.ReligiousDenomination,
        u.AccountType,
        u.Name
    FROM
        Users u
    WHERE
        u.AccountType = 'GABBAI';
END;



create
    definer = dbmasteruser@`%` procedure GetAvailableShuls()
BEGIN
    SELECT
        ShulID,
        ShulName,
        Location,
        Denomination,
        ContactInfo,
        ShachrisTime,
        MinchaTime,
        MaarivTime
    FROM
        Shuls;
END;



create
    definer = dbmasteruser@`%` procedure GetEventsForShul(IN shulId varchar(255))
BEGIN
    SELECT
        SE.EventID,
        SE.ShulID,
        SE.EventName,
        SE.TimeOfEvent,
        SE.Location,
        SE.Subscription,
        SE.Description
    FROM
        ShulEvents SE
    WHERE
        SE.ShulID = shulId;
END;



create
    definer = dbmasteruser@`%` procedure GetGabbaiRequestsForAdmin()
BEGIN
    SELECT
        g.RequestID,
        g.RequestName,
        g.Location,
        g.Denomination,
        g.ContactInfo,
        g.ShachrisTime,
        g.MinchaTime,
        g.MaarivTime,
        g.ApprovalStatus,
        g.UserID
    FROM
        GabbaiShulRequests g;
    
END;



create
    definer = dbmasteruser@`%` procedure GetGabbaiRequestsForGabbai(IN NewGabbaiID int)
BEGIN
    SELECT
        GabbaiShulRequests.RequestID,
        GabbaiShulRequests.GabbaiID,
        GabbaiShulRequests.ApprovalStatus,
        GabbaiShulRequests.RequestName,
        GabbaiShulRequests.Location,
        GabbaiShulRequests.Denomination,
        GabbaiShulRequests.ContactInfo,
        GabbaiShulRequests.ShachrisTime,
        GabbaiShulRequests.MinchaTime,
        GabbaiShulRequests.MaarivTime,
        GabbaiShulRequests.ApprovalStatus
    FROM
        GabbaiShulRequests
    WHERE NewGabbaiID = UserID
    ;

END;



create
    definer = dbmasteruser@`%` procedure GetGabbaiShulByRequestID(IN inputRequestID int)
BEGIN
    SELECT
        RequestID,
        GabbaiID,
        ApprovalStatus,
        RequestName,
        Location,
        Denomination,
        ContactInfo,
        ShachrisTime,
        MinchaTime,
        MaarivTime
    FROM
        GabbaiShulRequests
    WHERE inputRequestID = RequestID
    ;

END;



create
    definer = dbmasteruser@`%` procedure GetGabbaiShuls(IN userId int)
BEGIN
    SELECT
        sh.ShulID,
        sh.ShulName,
        sh.Location,
        sh.Denomination,
        sh.ContactInfo,
        sh.ShachrisTime,
        sh.MinchaTime,
        sh.MaarivTime
    FROM
        Shuls sh
    INNER JOIN Gabbai g ON sh.ShulID = g.ShulID
    WHERE
        g.UserID = userId;
END;



create
    definer = dbmasteruser@`%` procedure GetInitiatedGabbaiShul(IN NewRequestID int, IN NewName varchar(255),
                                                                IN NewLocation varchar(255),
                                                                IN NewDenomination varchar(255), IN NewContactInfo text,
                                                                IN NewShachrisTime varchar(255),
                                                                IN NewMinchaTime varchar(255),
                                                                IN NewMaarivTime varchar(255),
                                                                IN TheGabbaiID varchar(255))
BEGIN
    INSERT INTO GabbaiShulRequests (RequestID, RequestName, Location, Denomination, ContactInfo, ShachrisTime, MinchaTime, MaarivTime, UserID, ApprovalStatus)
    VALUES (NewRequestID, NewName, NewLocation, NewDenomination, NewContactInfo, NewShachrisTime, NewMinchaTime, NewMaarivTime, TheGabbaiID, 'Awaiting Decision');
END;

create
    definer = dbmasteruser@`%` procedure GetUserByPassword(IN inputUsername varchar(255), IN inputPassword varchar(255))
BEGIN

    SELECT
        u.UserID,
        u.Username,
        u.DateOfBirth,
        u.ReligiousDenomination,
        u.AccountType,
        u.Name,
        s.ShulID,
        s.ShulName,
        s.Location,
        s.Denomination,
        s.ContactInfo,
        s.ShachrisTime,
        s.MinchaTime,
        s.MaarivTime
    FROM
        Users u
        LEFT JOIN FollowedShuls fs ON u.UserID = fs.UserID
        LEFT JOIN Shuls s ON fs.ShulID = s.ShulID
    WHERE
        u.Username = inputUsername AND u.Password = inputPassword;

    SELECT
        SE.EventID,
        SE.ShulID,
        SE.EventName,
        SE.TimeOfEvent,
        SE.Location,
        SE.Subscription,
        SE.Description
    FROM
        ShulEvents SE
    WHERE
        SE.ShulID IN (SELECT ShulID FROM FollowedShuls WHERE UserID = (SELECT UserID FROM Users WHERE Username = inputUsername AND Password = inputPassword));
END;



create
    definer = dbmasteruser@`%` procedure GetUserSalt(IN inputUsername varchar(255))
BEGIN
    SELECT Salt FROM Users WHERE userName = inputUserName;
END;



create
    definer = dbmasteruser@`%` procedure MakeAdminDecision(IN NewRequestID int, IN NewApprovalStatus varchar(255))
BEGIN
    UPDATE GabbaiShulRequests
    SET ApprovalStatus = NewApprovalStatus
    WHERE RequestID = NewRequestID;
END;



create
    definer = dbmasteruser@`%` procedure RemoveShulFromUser(IN inputUserId int, IN inputShulId int)
BEGIN
    DELETE FROM FollowedShuls
    WHERE UserID = inputUserId AND ShulID = inputShulId;
END;



create
    definer = dbmasteruser@`%` procedure UpdateShulDetails(IN _ShulID int, IN UpdatedName varchar(255),
                                                           IN UpdatedLocation varchar(255),
                                                           IN UpdatedDenomination varchar(255),
                                                           IN UpdatedContactInfo text,
                                                           IN UpdatedShachrisTime varchar(255),
                                                           IN UpdatedMinchaTime varchar(255),
                                                           IN UpdatedMaarivTime varchar(255))
BEGIN
    UPDATE Shuls s
    SET
        ShulName = UpdatedName,
        Location = UpdatedLocation,
        Denomination = UpdatedDenomination,
        ContactInfo = UpdatedContactInfo,
        ShachrisTime = UpdatedShachrisTime,
        MinchaTime = UpdatedMinchaTime,
        MaarivTime = UpdatedMaarivTime

    WHERE
        ShulID = _ShulID;
END;



create
    definer = dbmasteruser@`%` procedure UpgradeUserToGabbai(IN _userId int)
BEGIN
    -- Update User column from USER to GABBAI
    update Users set AccountType = 'GABBAI'
    WHERE UserID = _userID;

END;
