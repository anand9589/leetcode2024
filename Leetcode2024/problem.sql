--570. Managers with at Least 5 Direct Reports
drop table if exists Employee
Create table Employee (id int, name varchar(255), department varchar(255), managerId int)
Truncate table Employee
insert into Employee (id, name, department, managerId) values ('101', 'John', 'A', NULL)
insert into Employee (id, name, department, managerId) values ('102', 'Dan', 'A', '101')
insert into Employee (id, name, department, managerId) values ('103', 'James', 'A', '101')
insert into Employee (id, name, department, managerId) values ('104', 'Amy', 'A', '101')
insert into Employee (id, name, department, managerId) values ('105', 'Anne', 'A', '101')
insert into Employee (id, name, department, managerId) values ('106', 'Ron', 'B', '101')

select e2.name from employee e1
join Employee e2 on e1.managerId = e2.id
group by e2.id,e2.name having count(*)>=5


--1075
drop table if exists Project
drop table if exists Employee
Create table Project (project_id int, employee_id int)
Create table Employee (employee_id int, name varchar(10), experience_years int)
Truncate table Project
insert into Project (project_id, employee_id) values (1, 1)
insert into Project (project_id, employee_id) values (1, 2)
insert into Project (project_id, employee_id) values (1, 3)
insert into Project (project_id, employee_id) values (2, 1)
insert into Project (project_id, employee_id) values (2, 4)
Truncate table Employee
insert into Employee (employee_id, name, experience_years) values (1, 'Khaled', 3)
insert into Employee (employee_id, name, experience_years) values (2, 'Ali', 2)
insert into Employee (employee_id, name, experience_years) values (3, 'John', 1)
insert into Employee (employee_id, name, experience_years) values (4, 'Doe', 2)

select p.project_id, cast( avg(e.experience_years * 1.0) as decimal(4,2)) as average_years  from Project p
left join Employee e on e.employee_id = p.employee_id
group by p.project_id


--1633
drop  table If Exists Users
drop  table If Exists Register
Create table Users (user_id int, user_name varchar(20))
Create table Register (contest_id int, user_id int)
Truncate table Users
insert into Users (user_id, user_name) values (6, 'Alice')
insert into Users (user_id, user_name) values (2, 'Bob')
insert into Users (user_id, user_name) values (7, 'Alex')
Truncate table Register
insert into Register (contest_id, user_id) values (215, 6)
insert into Register (contest_id, user_id) values (209, 2)
insert into Register (contest_id, user_id) values (208, 2)
insert into Register (contest_id, user_id) values (210, 6)
insert into Register (contest_id, user_id) values (208, 6)
insert into Register (contest_id, user_id) values (209, 7)
insert into Register (contest_id, user_id) values (209, 6)
insert into Register (contest_id, user_id) values (215, 7)
insert into Register (contest_id, user_id) values (208, 7)
insert into Register (contest_id, user_id) values (210, 2)
insert into Register (contest_id, user_id) values (207, 2)
insert into Register (contest_id, user_id) values (210, 7)

declare @userCount int 
set @userCount = (select count(user_id) from Users)

select contest_id,  CAST(((COUNT(user_id) * 1.0)/@userCount) * 100.0 as decimal(18,2)) as percentage  from Register 
group by contest_id
order by percentage desc, contest_id

--1211
drop table If Exists Queries
Create table Queries (query_name varchar(30), result varchar(50), position int, rating int)
Truncate table Queries
insert into Queries (result, position, rating) values ( 'Golden Retriever', 1, 5)
insert into Queries (result, position, rating) values ( 'German Shepherd', 2, 5)
insert into Queries (result, position, rating) values ('Mule', 200, 1)
insert into Queries (query_name, result, position, rating) values ('Cat', 'Shirazi', 5, 2)
insert into Queries (query_name, result, position, rating) values ('Cat', 'Siamese', 3, 3)
insert into Queries (query_name, result, position, rating) values ('Cat', 'Sphynx', 7, 4)

select *, rating*1.0/position, case when rating<3 then 1 else 0 end from Queries

select query_name, count(query_name), avg(rating*1.0/position),sum( case when rating<3 then 1 else 0 end) from Queries 
group  by query_name

--select query_name, sum(rating*1.0/position), sum( case when rating<3 then 1 else 0 end), count(query_name) from Queries
--group by query_name

select 
	query_name
	,CAST(avg(rating*1.0/position) as decimal(18,2)) as quality
	,cast( sum( case when rating<3 then 1 else 0 end) * 100.0 / count(query_name) as decimal(18,2)) as poor_query_percentage   
from Queries
group by query_name having query_name is not null


--1193
drop table if exists Transactions
Create table Transactions (id int, country varchar(4), state nchar(10), amount int, trans_date date)
Truncate table Transactions
insert into Transactions (id, country, state, amount, trans_date) values (121, 'US', 'approved', 1000, '2018-12-18')
insert into Transactions (id, country, state, amount, trans_date) values (122, 'US', 'declined', 2000, '2018-12-19')
insert into Transactions (id, country, state, amount, trans_date) values (123, 'US', 'approved', 2000, '2019-01-01')
insert into Transactions (id, country, state, amount, trans_date) values (124, 'DE', 'approved', 2000, '2019-01-07')


select 
	FORMAT(trans_date, 'yyyy-MM') as month
	,country
	,COUNT(state) as trans_count
	,sum(case when state='approved ' then 1 else 0 end) as   approved_count 
	,sum(amount) as trans_total_amount 
	,sum(case when state='approved ' then amount else 0 end ) as approved_total_amount 
from Transactions
group by FORMAT(trans_date, 'yyyy-MM'), country

--1174

drop table if Exists Delivery
Create table Delivery (delivery_id int, customer_id int, order_date date, customer_pref_delivery_date date)
Truncate table Delivery
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('1', '1', '2019-08-01', '2019-08-02')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('2', '2', '2019-08-02', '2019-08-02')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('3', '1', '2019-08-11', '2019-08-12')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('4', '3', '2019-08-24', '2019-08-24')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('5', '3', '2019-08-21', '2019-08-22')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('6', '2', '2019-08-11', '2019-08-13')
insert into Delivery (delivery_id, customer_id, order_date, customer_pref_delivery_date) values ('7', '4', '2019-08-09', '2019-08-09')
GO
with cte_delState AS (
select customer_id, case when order_date = customer_pref_delivery_date then 1 else 0 end as delievryStatus, ROW_NUMBER() over (partition by customer_id order by order_date) as rowNum from Delivery)

select CAST( (sum(delievryStatus) * 100.00) / count(delievryStatus) as decimal(18,2)) as immediate_percentage  from cte_delState where rowNum=1

--
drop table If Exists Activity
Create table Activity (player_id int, device_id int, event_date date, games_played int)
Truncate table Activity
insert into Activity (player_id, device_id, event_date, games_played) values (1, 2, '2016-03-01', 5)
insert into Activity (player_id, device_id, event_date, games_played) values (1, 2, '2016-03-02', 6)
insert into Activity (player_id, device_id, event_date, games_played) values (2, 3, '2017-06-25', 1)
insert into Activity (player_id, device_id, event_date, games_played) values (3, 1, '2016-03-02', 0)
insert into Activity (player_id, device_id, event_date, games_played) values (3, 4, '2018-07-03', 5)
GO

WITH CTE AS (
SELECT
player_id, min(event_date) as event_start_date
from
Activity
group by player_id )

SELECT
cast(((count(distinct c.player_id) *1.0) / (select count(distinct player_id) from activity)) as decimal (18,2))as fraction
FROM
CTE c
JOIN Activity a
on c.player_id = a.player_id
and datediff(day,c.event_start_date, a.event_date) = 1
GO

with cte As(
	select 
		player_id
		,event_date
		,ROW_NUMBER() OVER(partition by player_id order by  event_date) as row 
	from Activity
)

select 
	CAST( COUNT(c.player_id) *1.0 / count(distinct a.player_id) as decimal(4,2)) as fraction  
from 
	activity a
left join cte c on c.player_id = a.player_id and c.row = 1 and DATEDIFF(day,a.event_date,c.event_date)=-1



drop table If Exists Teacher
Create table Teacher (teacher_id int, subject_id int, dept_id int)
Truncate table Teacher
insert into Teacher (teacher_id, subject_id, dept_id) values (1, 2, 3)
insert into Teacher (teacher_id, subject_id, dept_id) values (1, 2, 4)
insert into Teacher (teacher_id, subject_id, dept_id) values (1, 3, 3)
insert into Teacher (teacher_id, subject_id, dept_id) values (2, 1, 1)
insert into Teacher (teacher_id, subject_id, dept_id) values (2, 2, 1)
insert into Teacher (teacher_id, subject_id, dept_id) values (2, 3, 1)
insert into Teacher (teacher_id, subject_id, dept_id) values (2, 4, 1)

select teacher_id, COUNT(distinct subject_id) as cnt from Teacher
group by teacher_id

--1045. Customers Who Bought All Products

drop table if exists Customer
go
drop table if exists Product
go

Create table Customer (customer_id int, product_key int)
Create table Product (product_key int)
Truncate table Customer
insert into Customer (customer_id, product_key) values ('1', '5')
insert into Customer (customer_id, product_key) values ('2', '6')
insert into Customer (customer_id, product_key) values ('3', '5')
insert into Customer (customer_id, product_key) values ('3', '6')
insert into Customer (customer_id, product_key) values ('1', '6')
Truncate table Product
insert into Product (product_key) values ('5')
insert into Product (product_key) values ('6')


declare @productCount int = (select count(product_key) from Product)

select customer_id from Customer
group by customer_id having count(distinct product_key ) = @productCount

--1731. The Number of Employees Which Report to Each Employee
drop table If Exists Employees
GO
Create table Employees(employee_id int, name varchar(20), reports_to int, age int)
Truncate table Employees
insert into Employees (employee_id, name, reports_to, age) values (9, 'Hercy', NULL, 43)
insert into Employees (employee_id, name, reports_to, age) values (6, 'Alice', 9, 41)
insert into Employees (employee_id, name, reports_to, age) values (4, 'Bob', 9, 36)
insert into Employees (employee_id, name, reports_to, age) values (2, 'Winston', NULL, 37)
GO

select 
	e1.employee_id, 
	e1.name,
	count(e2.employee_id) as reports_count, 
	CAST(avg(e2.age * 1.0) as decimal(4,0)) as average_age  
from Employees e1
inner join Employees e2 on e1.employee_id = e2.reports_to
group by e1.employee_id, e1.name
order by e1.employee_id


--1789. Primary Department for Each Employee
drop table if exists employee
go

Create table Employee (employee_id int, department_id int, primary_flag nchar(1))
Truncate table Employee
insert into Employee (employee_id, department_id, primary_flag) values (1, 1, 'N')
insert into Employee (employee_id, department_id, primary_flag) values (2, 1, 'Y')
insert into Employee (employee_id, department_id, primary_flag) values (2, 2, 'N')
insert into Employee (employee_id, department_id, primary_flag) values (3, 3, 'N')
insert into Employee (employee_id, department_id, primary_flag) values (4, 2, 'N')
insert into Employee (employee_id, department_id, primary_flag) values (4, 3, 'Y')
insert into Employee (employee_id, department_id, primary_flag) values (4, 4, 'N')


select employee_id, department_id from Employee
where primary_flag = 'Y'
UNION
select employee_id, min(department_id) as department_id from Employee
group by employee_id having count( department_id)=1


 SELECT 
	employee_id, 
	department_id 
FROM (
	select 
		employee_id, 
		department_id, 
		ROW_NUMBER() OVER (PARTITION BY employee_id ORDER BY primary_flag DESC) as ROWNUMBER 
	from Employee) s
 WHERE S.ROWNUMBER = 1