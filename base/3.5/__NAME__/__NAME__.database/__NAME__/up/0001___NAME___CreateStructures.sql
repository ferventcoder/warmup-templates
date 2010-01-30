
/*
CREATE TABLE dbo.ReceivedEmails 
(
	Id					UniqueIdentifier	NOT NULL
	,message_id			NVarChar(255)		NULL
	,delivery_date		DateTime			NULL
	,from_address		NVarChar(255)		NULL
	,to_addresses		NVarChar(500)		NULL
	,cc_addresses		NVarChar(500)		NULL
	,bcc_addresses		NVarChar(500)		NULL
	,priority			NVarChar(20)		NULL
	,subject			NVarChar(500)		NULL
	,message_body		NText				NULL
	--auditing purposes
	,entered_date		DateTime			NULL	DEFAULT(GetDate())
	,modified_date		DateTime			NULL	DEFAULT(GetDate())
	,updating_user		NVarChar(50)		NULL
	,CONSTRAINT [PK_ReceivedEmails_Id] PRIMARY KEY CLUSTERED (Id)
)
*/