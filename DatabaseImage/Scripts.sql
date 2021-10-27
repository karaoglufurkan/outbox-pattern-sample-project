create table OutboxEvents
(
    Id           bigint identity
        constraint OutboxEvents_pk
            primary key nonclustered,
    Data         nvarchar(max),
    Type         nvarchar(max),
    EventId      uniqueidentifier not null,
    EventDate    datetime,
    State        int,
    ModifiedDate datetime
)
go

create table Orders
(
    IsCancelled bit     default 0 not null,
    Email       nvarchar(50),
    Id          int identity
        constraint Orders_pk
            primary key nonclustered,
    UserId      int               not null,
    TotalPrice  decimal default 0 not null
)
go

create table MailQueue
(
    Id    bigint identity
        constraint MailQueue_pk
            primary key nonclustered,
    Email nvarchar(50),
    Title nvarchar(100),
    Body  nvarchar(max),
    State int default 0 not null
)
go

create unique index MailQueue_Id_uindex
    on MailQueue (Id)
go

