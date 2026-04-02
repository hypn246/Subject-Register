Use DKM
INSERT INTO Major (Id, Name) VALUES
(1, 'Computer Science'),
(2, 'Information Technology'),
(3, 'Software Engineering'),
(4, 'Data Science'),
(5, 'Cyber Security'),
(6, 'Artificial Intelligence'),
(7, 'Business Administration'),
(8, 'Finance'),
(9, 'Marketing'),
(10, 'Economics');
INSERT INTO [User] (Id, Name, Username, Password, Role, MajorId) VALUES
(1, 'StudentOne', 'stu1', '123', 2, 1),
(2, 'Lecturer1', 'lec1', '123', 2, 2),
(3, 'Admin', 'admin', '123', 2, 3);
INSERT INTO Course (Id, Credit) VALUES
(1, 3),
(2, 4),
(3, 3),
(4, 2),
(5, 3),
(6, 4),
(7, 3),
(8, 2),
(9, 3),
(10, 4);
INSERT INTO Assign (Id, UserId, CourseId) VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 4),
(5, 5, 5),
(6, 6, 6),
(7, 7, 7),
(8, 8, 8),
(9, 9, 9),
(10, 10, 10);
INSERT INTO ScoreTable (Id, GPA) VALUES
(1, 3.2),
(2, 3.5),
(3, 2.8),
(4, 3.0),
(5, 3.7),
(6, 2.9),
(7, 3.4),
(8, 3.1),
(9, 3.6),
(10, 2.7);
INSERT INTO Score (Id, ProgressTerm, MidTerm, FinalTerm, Status) VALUES
(1, 8, 7, 9, 1),
(2, 7, 6, 8, 1),
(3, 6, 5, 7, 1),
(4, 9, 8, 9, 1),
(5, 5, 6, 7, 1),
(6, 7, 7, 8, 1),
(7, 8, 9, 9, 1),
(8, 6, 6, 7, 1),
(9, 7, 8, 8, 1),
(10, 5, 5, 6, 0);
