USE [TeorijaIgara]
GO
/****** Object:  Table [dbo].[Statistics]    Script Date: 1/15/2019 10:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statistics](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HomeStudentId] [bigint] NULL,
	[GuestStudentId] [bigint] NULL,
	[HomePoints] [int] NULL,
	[GuestPoints] [int] NULL,
	[HomeScore] [int] NULL,
	[GuestScore] [int] NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [PK_Statistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 1/15/2019 10:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [smallint] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Statistics]  WITH CHECK ADD  CONSTRAINT [FK_Statistics_Student] FOREIGN KEY([GuestStudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Statistics] CHECK CONSTRAINT [FK_Statistics_Student]
GO
ALTER TABLE [dbo].[Statistics]  WITH CHECK ADD  CONSTRAINT [FK_Statistics_Student1] FOREIGN KEY([HomeStudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Statistics] CHECK CONSTRAINT [FK_Statistics_Student1]
GO
/****** Object:  StoredProcedure [dbo].[usp_PerUser]    Script Date: 1/15/2019 10:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_PerUser] 
	-- Add the parameters for the stored procedure here
	@studentId as bigint = NULL,
	@type as smallint = NULL

AS
BEGIN
	Select t.HomeStudentId as [HomeStudentId],
	t.GuestStudentId as [GuestStudentId],
	s1.Name as [HomeStudentName],
	s2.Name as [GuestStudentName],
	sum(t.Win) as [Win],	 
	sum(t.Draw) as [Draw],	 
	sum(t.Lost) as [Lost]
	from(
	select HomeStudentId as [HomeStudentId],
	GuestStudentId as [GuestStudentId],
	sum(case when HomePoints = 3 then 1 else 0 end) as [Win],	 
	sum(case when HomePoints = 1 then 1 else 0 end) as [Draw],	 
	sum(case when HomePoints = 0 then 1 else 0 end) as [Lost]
	from [Statistics]
	where HomeStudentId = @studentId or @studentId is null
	group by HomeStudentId,GuestStudentId
	union
	select GuestStudentId as [HomeStudentId],
	HomeStudentId as [GuestStudentId],
	sum(case when GuestPoints = 3 then 1 else 0 end) as [Win],	 
	sum(case when GuestPoints = 1 then 1 else 0 end) as [Draw],	 
	sum(case when GuestPoints = 0 then 1 else 0 end) as [Lost]
	from [Statistics]
	where GuestStudentId = @studentId or @studentId is null
	group by GuestStudentId,HomeStudentId
	)t
	inner join Student s1 on t.HomeStudentId = s1.Id
	inner join Student s2 on t.GuestStudentId =s2.Id
	where (s1.Type = @type and s2.Type = @type) or @type is null
	group by t.HomeStudentId,t.GuestStudentId,s1.Name, s2.Name
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Statistics]    Script Date: 1/15/2019 10:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Statistics]

	@type as smallint = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select t.StudentId as [StudentId],
	s.[Name] as [Student],
	sum(t.Points) as [Points],
	sum(t.Score) as [Score],
	sum(t.Win) as [Win],
	sum(t.Draw) as [Draw],
	sum(t.Lost) as [Lost]
	from(
	select HomeStudentId as [StudentId],
	sum(HomePoints) as [Points],
	sum(HomeScore) as [Score],
	sum(case when HomePoints = 3 then 1 else 0 end) as [Win],	 
	sum(case when HomePoints = 1 then 1 else 0 end) as [Draw],	 
	sum(case when HomePoints = 0 then 1 else 0 end) as [Lost]
	from [Statistics] 
	group by HomeStudentId
	union
	select GuestStudentId as [StudentId],
	sum(GuestPoints) as [Points],
	sum(GuestScore) as [Score],
	sum(case when GuestPoints = 3 then 1 else 0 end) as [Win],	 
	sum(case when GuestPoints = 1 then 1 else 0 end) as [Draw],	 
	sum(case when GuestPoints = 0 then 1 else 0 end) as [Lost]
	from [Statistics] 
	group by GuestStudentId
	) t
	inner join Student s on t.StudentId = s.Id
	where s.Type = @type or @type is null
	group by t.StudentId,s.[Name]
	order by Points desc
    -- Insert statements for procedure here
	
END
GO
