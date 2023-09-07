IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230904215510_initial migration')
BEGIN
    CREATE TABLE [Accounts] (
        [Id] uniqueidentifier NOT NULL,
        [AccountNumber] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Balance] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230904215510_initial migration')
BEGIN
    CREATE TABLE [Transactions] (
        [Id] uniqueidentifier NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [ReceiverAccountNumber] int NOT NULL,
        [TransactionTime] datetime2 NOT NULL,
        [SenderAccountId] uniqueidentifier NOT NULL,
        [AccountId] uniqueidentifier NULL,
        CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transactions_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230904215510_initial migration')
BEGIN
    CREATE INDEX [IX_Transactions_AccountId] ON [Transactions] ([AccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230904215510_initial migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230904215510_initial migration', N'7.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230905160334_rename     public class AccountTransaction')
BEGIN
    DROP TABLE [Transactions];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230905160334_rename     public class AccountTransaction')
BEGIN
    CREATE TABLE [AccountTransactions] (
        [Id] uniqueidentifier NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [ReceiverAccountNumber] int NOT NULL,
        [TransactionTime] datetime2 NOT NULL,
        [SenderAccountId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AccountTransactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccountTransactions_Accounts_SenderAccountId] FOREIGN KEY ([SenderAccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230905160334_rename     public class AccountTransaction')
BEGIN
    CREATE INDEX [IX_AccountTransactions_SenderAccountId] ON [AccountTransactions] ([SenderAccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230905160334_rename     public class AccountTransaction')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230905160334_rename     public class AccountTransaction', N'7.0.10');
END;
GO

COMMIT;
GO

