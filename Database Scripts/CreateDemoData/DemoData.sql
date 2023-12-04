USE QUANLYBENHVIEN



-------------------------INSERT TAI KHOAN------------------------------
USE QUANLYBENHVIEN

INSERT INTO TAIKHOAN VALUES ('nguyenvanbinhNV001','bvuFbrUHtRMQaNXqQKR8Hg==','NV001', N'Tiếp tân', '/Image/User/1.png')
INSERT INTO TAIKHOAN VALUES ('tranthuhangM002','ZW8AH23g7cVKMtrGhDay7A==','M002',N'Bác sĩ', '/Image/User/2.png')
INSERT INTO TAIKHOAN VALUES ('ngoclinhhanNV008','mmCW7Puz0jtC9wOzkEWZ2Q==','AD008', N'Admin', '/Image/User/3.png')


USE QUANLYBENHVIEN

------------INSERT PHONG-------------------------------
INSERT INTO PHONG VALUES (1,'Phòng A',10)
INSERT INTO PHONG VALUES (3,'Phòng B',15)
INSERT INTO PHONG VALUES (4,'Phòng C',20)
INSERT INTO PHONG VALUES (7,'Phòng D',12)
INSERT INTO PHONG VALUES (9,'Phòng E',18)

GO

USE QUANLYBENHVIEN

SET DATEFORMAT DMY

--------------------INSERT KHOA-------------------------
INSERT INTO KHOA VALUES (N'Khoa Nội Tiêu Hóa','12/05/2020',null, '/Image/Khoa/1.png')
INSERT INTO KHOA VALUES (N'Khoa Tim Mạch','30/10/2019', null, '/Image/Khoa/2.png')
INSERT INTO KHOA VALUES (N'Khoa Ngoại Chấn Thương','20/03/2021', null, '/Image/Khoa/3.png')
INSERT INTO KHOA VALUES (N'Khoa Phục Hồi Chức Năng','15/01/2022', null, '/Image/Khoa/4.png')


GO

USE QUANLYBENHVIEN

SET DATEFORMAT DMY

---------------------INSERT YSI VALUES ------------------------
INSERT INTO YSI VALUES ( '1', '1', N'Nguyễn Văn An', 'Nam', '03/03/2001', '12/03/2021', N'Bác sĩ chuyên khoa', null)
INSERT INTO YSI VALUES ( '2', '2', N'Nguyễn Văn Thanh', 'Nam', '11/02/1990', '10/09/2022', N'Điều dưỡng', null)
INSERT INTO YSI VALUES ('3', '3', N'Trần Kim Tuyền', N'Nữ', '09/02/1992', '08/01/2021', N'Bác sĩ chuyên khoa', null)
INSERT INTO YSI VALUES ( '4', '4', N'Trần Văn Thành', 'Nam', '12/12/1995', '18/02/2021', N'Bác sĩ chuyên khoa', null)
INSERT INTO YSI VALUES ( '2', '5', N'Bùi Văn Nam', 'Nam', '19/12/1998', '18/08/2023', N'Bác sĩ chuyên khoa', null)


UPDATE KHOA SET TRUONGKHOA = '1' WHERE (MAKHOA = '1')
UPDATE KHOA SET TRUONGKHOA = '5' WHERE (MAKHOA = '2')
UPDATE KHOA SET TRUONGKHOA = '3' WHERE (MAKHOA = '3')
UPDATE KHOA SET TRUONGKHOA = '4' WHERE (MAKHOA = '4')


UPDATE YSI SET MACHIHUY = '1' WHERE (MAYSI = '2')

USE QUANLYBENHVIEN
----------------------INSERT BENHNHAN---------------------------
INSERT BENHNHAN (HOTEN, GIOITINH, MAPHONG, NGAYSINH, NGAYNHAPVIEN, DIACHI, MABHYT) VALUES (N'Nguyễn Văn An','Nam','1','12/5/2000','12/4/2021',N'Quận x thành phố y','BH001')
INSERT BENHNHAN (HOTEN, GIOITINH, MAPHONG, NGAYSINH, NGAYNHAPVIEN, DIACHI, MABHYT) VALUES (N'Trần Thị Thủy',N'Nữ','2','23/5/1999','2/4/2022',N'Quận x thành phố y','BH002')
INSERT BENHNHAN (HOTEN, GIOITINH, MAPHONG, NGAYSINH, NGAYNHAPVIEN, DIACHI, MABHYT) VALUES (N'Lê Văn Cẩn',N'Nam','3','24/3/2010','13/4/2023',N'Quận x thành phố y','BH003')
INSERT BENHNHAN (HOTEN, GIOITINH, MAPHONG, NGAYSINH, NGAYNHAPVIEN, DIACHI, MABHYT) VALUES (N'Phạm Thị Thúy',N'Nữ','4','12/5/2005','31/1/2021',N'Quận x thành phố y','BH004')

GO  

-----------------------INSERT DICH VU-----------------------------------
INSERT INTO DICHVU VALUES (N'Khám chữa bệnh',50000, '/Image/DichVu/1.png')
INSERT INTO DICHVU VALUES (N'Xét nghiệm',10000, '/Image/DichVu/1.png')
INSERT INTO DICHVU VALUES (N'Hồi sức',200000, '/Image/DichVu/1.png')


USE QUANLYBENHVIEN

----------------------INSERT BENH AN-----------------------------
INSERT INTO BENHAN VALUES ('1','1','2',N'Trĩ nội, cần theo dõi thêm',10000)
INSERT INTO BENHAN VALUES ('3','2','1',N'Viêm hô hấp cấp',50000)
INSERT INTO BENHAN VALUES ('5','3','1',N'Viêm xoang',10000)
INSERT INTO BENHAN VALUES ('3','4','3',N'Run vô căn',200000)


GO


-------------------------INSERT LICH KHAM------------------------

INSERT INTO LICHKHAM VALUES ('1','1','1','1','12/3/2022','10/3/2022')
INSERT INTO LICHKHAM VALUES ('2','2','2','2','15/5/2023','10/5/2023')
INSERT INTO LICHKHAM VALUES ('3','3','3','3','14/6/2021','10/6/2021')
INSERT INTO LICHKHAM VALUES ('4','4','4','2','12/5/2020','10/5/2020')

GO

-----------------------INSERT DON THUOC-----------------------------

INSERT INTO DONTHUOC VALUES ('1',N'1 vỉ thuốc A, uống sau mỗi bữa ăn')
INSERT INTO DONTHUOC VALUES ('2',N'12 ống B, uống vào mỗi sáng')
INSERT INTO DONTHUOC VALUES ('3',N'12 viên thuốc C, uống 2 ngày một lần')
INSERT INTO DONTHUOC VALUES ('4',N'6 viên thuốc D, uống trong 1 tuần')

GO
