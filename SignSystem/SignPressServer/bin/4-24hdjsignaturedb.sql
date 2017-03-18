/*
Navicat MySQL Data Transfer

Source Server         : localhost_3333
Source Server Version : 50529
Source Host           : localhost:3333
Source Database       : hdjsignaturedb

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2016-04-24 10:15:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '工程名称id',
  `category` varchar(255) DEFAULT NULL COMMENT '工程名称',
  `shortcall` varchar(255) DEFAULT NULL COMMENT '工程名称简称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('1', '界河航道例行养护工程', '界例');
INSERT INTO `category` VALUES ('2', '界河航道专项养护工程', '界专');
INSERT INTO `category` VALUES ('3', '内河航道例行养护工程', '内例');
INSERT INTO `category` VALUES ('4', '内河航道专项养护工程', '内专');

-- ----------------------------
-- Table structure for conidcategory
-- ----------------------------
DROP TABLE IF EXISTS `conidcategory`;
CREATE TABLE `conidcategory` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `departmentid` int(11) DEFAULT NULL,
  `categoryid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `departmentid` (`departmentid`),
  KEY `categoryid` (`categoryid`),
  CONSTRAINT `conidcategory_ibfk_1` FOREIGN KEY (`departmentid`) REFERENCES `department` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `conidcategory_ibfk_2` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of conidcategory
-- ----------------------------
INSERT INTO `conidcategory` VALUES ('1', '1', '1');
INSERT INTO `conidcategory` VALUES ('2', '1', '2');
INSERT INTO `conidcategory` VALUES ('3', '1', '3');
INSERT INTO `conidcategory` VALUES ('4', '1', '4');

-- ----------------------------
-- Table structure for contemp
-- ----------------------------
DROP TABLE IF EXISTS `contemp`;
CREATE TABLE `contemp` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '会签单模版编号',
  `createdate` datetime NOT NULL COMMENT '创建时间',
  `name` varchar(255) NOT NULL COMMENT '会签单模版名称',
  `type` int(11) NOT NULL COMMENT '模版类型',
  `column1` varchar(255) NOT NULL COMMENT '栏目1',
  `column2` varchar(255) NOT NULL COMMENT '栏目2',
  `column3` varchar(255) NOT NULL COMMENT '栏目3',
  `column4` varchar(255) NOT NULL COMMENT '栏目4',
  `column5` varchar(255) NOT NULL COMMENT '栏目5',
  `column6` varchar(255) DEFAULT NULL COMMENT '栏目6',
  `signinfo1` varchar(255) NOT NULL COMMENT '签字人1信息',
  `signinfo2` varchar(255) NOT NULL COMMENT '签字人2信息',
  `signinfo3` varchar(255) NOT NULL COMMENT '签字人3信息',
  `signinfo4` varchar(255) NOT NULL COMMENT '签字人4信息',
  `signinfo5` varchar(255) NOT NULL COMMENT '签字人5信息',
  `signinfo6` varchar(255) NOT NULL COMMENT '签字人6信息',
  `signinfo7` varchar(255) NOT NULL COMMENT '签字人7信息',
  `signinfo8` varchar(255) NOT NULL COMMENT '签字人8信息',
  `signinfo9` varchar(255) DEFAULT NULL COMMENT '签字人9信息',
  `signinfo10` varchar(255) DEFAULT NULL COMMENT '签字人10信息',
  `signinfo11` varchar(255) DEFAULT NULL COMMENT '签字人11信息',
  `signinfo12` varchar(255) DEFAULT NULL COMMENT '签字人12信息',
  `signid1` int(11) NOT NULL COMMENT '签字人1的员工号',
  `signid2` int(11) NOT NULL COMMENT '签字人2的员工号',
  `signid3` int(11) NOT NULL COMMENT '签字人3的员工号',
  `signid4` int(11) NOT NULL COMMENT '签字人4的员工号',
  `signid5` int(11) NOT NULL COMMENT '签字人5的员工号',
  `signid6` int(11) NOT NULL COMMENT '签字人6的员工号',
  `signid7` int(11) NOT NULL COMMENT '签字人7的员工号',
  `signid8` int(11) NOT NULL COMMENT '签字人8的员工号',
  `signid9` int(11) DEFAULT '0' COMMENT '签字人9的员工号',
  `signid10` int(11) DEFAULT '0' COMMENT '签字人10的员工号',
  `signid11` int(11) DEFAULT '0' COMMENT '签字人11的员工号',
  `signid12` int(11) DEFAULT NULL COMMENT '签字人12的员工号',
  `signlevel1` int(11) DEFAULT NULL COMMENT '签字人1的签字顺序',
  `signlevel2` int(11) DEFAULT NULL COMMENT '签字人2的签字顺序',
  `signlevel3` int(11) DEFAULT NULL COMMENT '签字人3的签字顺序',
  `signlevel4` int(11) DEFAULT NULL COMMENT '签字人4的签字顺序',
  `signlevel5` int(11) DEFAULT NULL COMMENT '签字人5的签字顺序',
  `signlevel6` int(11) DEFAULT NULL COMMENT '签字人6的签字顺序',
  `signlevel7` int(11) DEFAULT NULL COMMENT '签字人7的签字顺序',
  `signlevel8` int(11) DEFAULT NULL COMMENT '签字人8的签字顺序',
  `signlevel9` int(11) DEFAULT NULL COMMENT '签字人9的签字顺序',
  `signlevel10` int(11) DEFAULT NULL COMMENT '签字人10的签字顺序',
  `signlevel11` int(11) DEFAULT NULL COMMENT '签字人11的签字顺序',
  `signlevel12` int(11) DEFAULT NULL COMMENT '签字人12的签字顺序',
  `canview1` int(11) DEFAULT NULL COMMENT '签字人1是否可以查看',
  `canview2` int(11) DEFAULT NULL,
  `canview3` int(11) DEFAULT NULL,
  `canview4` int(11) DEFAULT NULL,
  `canview5` int(11) DEFAULT NULL,
  `canview6` int(11) DEFAULT NULL,
  `canview7` int(11) DEFAULT NULL,
  `canview8` int(11) DEFAULT NULL,
  `canview9` int(11) DEFAULT NULL,
  `canview10` int(11) DEFAULT NULL,
  `canview11` int(11) DEFAULT NULL,
  `canview12` int(11) DEFAULT NULL,
  `candownload1` int(11) DEFAULT NULL,
  `candownload2` int(11) DEFAULT NULL,
  `candownload3` int(11) DEFAULT NULL,
  `candownload4` int(11) DEFAULT NULL,
  `candownload5` int(11) DEFAULT NULL,
  `candownload6` int(11) DEFAULT NULL,
  `candownload7` int(11) DEFAULT NULL,
  `candownload8` int(11) DEFAULT NULL,
  `candownload9` int(11) DEFAULT NULL,
  `candownload10` int(11) DEFAULT NULL,
  `candownload11` int(11) DEFAULT NULL,
  `candownload12` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of contemp
-- ----------------------------
INSERT INTO `contemp` VALUES ('1', '2016-04-23 21:59:08', '内河例行', '10', '工程名', '项目名', '工程量', '本次申请金额', '累计申请金额', '主办单位审核意见', '签字人一', '签字人二', '签字人三', '签字人四', '签字人五', '签字人六', '签字人七', '签字人八', null, null, null, null, '1', '2', '3', '5', '6', '7', '9', '10', '0', '0', '0', null, '1', '1', '1', '1', '1', '1', '1', '1', null, null, null, null, '1', '1', '0', '1', '0', '0', '1', '1', null, null, null, null, '1', '1', '0', '1', '0', '0', '1', '1', null, null, null, null);
INSERT INTO `contemp` VALUES ('2', '2016-04-23 22:21:32', '测试界河专项', '21', '1', '2', '3', '3', '4', null, '5', '12', '6', '3', '78', '2', '8', '3', '9', '4', '0', '4', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '部门id',
  `name` varchar(255) NOT NULL COMMENT '部门名称',
  `shortcall` varchar(255) DEFAULT NULL COMMENT '部门名称简称',
  `canboundary` int(11) DEFAULT '0' COMMENT '是否能申请界河项目',
  `caninland` int(11) DEFAULT '0' COMMENT '是否能申请内河项目',
  `higndepid` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`) USING BTREE,
  KEY `higndepid` (`higndepid`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('1', '黑龙江省航道局', '省', '1', '1', '1');
INSERT INTO `department` VALUES ('2', '监理单位', '监', '0', '0', '0');
INSERT INTO `department` VALUES ('3', '航道处', '处', '0', '0', '0');
INSERT INTO `department` VALUES ('4', '财务处', '财', '0', '0', '0');

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '员工id',
  `name` varchar(255) NOT NULL COMMENT '员工姓名',
  `position` varchar(255) NOT NULL COMMENT '员工职位',
  `departmentid` int(11) NOT NULL COMMENT '员工所属部门id',
  `isadmin` int(11) NOT NULL COMMENT '是否是管理员',
  `cansubmit` int(11) NOT NULL COMMENT '是否可以提交',
  `cansign` int(11) NOT NULL COMMENT '是否可以签字',
  `canstatistic` int(11) DEFAULT '0' COMMENT '是否可以统计',
  `username` varchar(255) NOT NULL COMMENT '登录用户名',
  `password` varchar(255) NOT NULL COMMENT '登录密码',
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  KEY `departmentid` (`departmentid`),
  CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`departmentid`) REFERENCES `department` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('1', '成坚', '负责人', '1', '1', '1', '1', '1', 'chengjian', 'chengjian');
INSERT INTO `employee` VALUES ('2', '李香元', '负责人', '1', '1', '1', '1', '1', 'lixiangyuan', 'lixiangyuan');
INSERT INTO `employee` VALUES ('3', '张三', '科长', '1', '0', '1', '0', '0', 'zhangsan', 'zhangsan');
INSERT INTO `employee` VALUES ('4', '李四', '局长', '1', '1', '1', '1', '1', 'lisi', 'lisi');
INSERT INTO `employee` VALUES ('5', '石啸', '负责人', '2', '0', '0', '0', '1', 'shixiao', 'shixiao');
INSERT INTO `employee` VALUES ('6', '赵强', '科长', '3', '0', '0', '1', '0', 'zhaoqiang', 'zhaoqiang');
INSERT INTO `employee` VALUES ('7', '许伟', '处长', '3', '0', '0', '1', '1', 'xuwei', 'xuwei');
INSERT INTO `employee` VALUES ('8', '王盼盼', '副局长', '3', '1', '0', '0', '1', 'wangpanpan', 'wangpanpan');
INSERT INTO `employee` VALUES ('9', '吴佳怡', '局长', '3', '1', '0', '0', '1', 'wujiayi', 'wujiayi');
INSERT INTO `employee` VALUES ('10', '凌煊峰', '负责人', '4', '0', '0', '0', '1', 'lingxuanfeng', 'lingxuanfeng');
INSERT INTO `employee` VALUES ('11', '张琦', '负责人', '4', '0', '0', '0', '1', 'zhangqi', 'zhangqi');
INSERT INTO `employee` VALUES ('12', '丁一', '负责人', '4', '1', '1', '1', '1', 'dingyi', 'dingyi');

-- ----------------------------
-- Table structure for hgjcontract
-- ----------------------------
DROP TABLE IF EXISTS `hgjcontract`;
CREATE TABLE `hgjcontract` (
  `id` varchar(255) NOT NULL COMMENT '提交的会签单的编号',
  `name` varchar(255) NOT NULL COMMENT '提交的会签单的名称',
  `contempid` int(11) NOT NULL COMMENT '当前会签单所使用的模版类',
  `columndata1` varchar(255) NOT NULL,
  `columndata2` varchar(255) NOT NULL,
  `columndata3` varchar(255) NOT NULL,
  `columndata4` varchar(255) NOT NULL,
  `columndata5` varchar(255) NOT NULL,
  `columndata6` varchar(255) NOT NULL,
  `subempid` int(11) NOT NULL COMMENT '申请人的id',
  `submitdate` datetime DEFAULT NULL COMMENT ' 提交时间',
  PRIMARY KEY (`id`),
  KEY `contempid` (`contempid`),
  KEY `subempid` (`subempid`),
  CONSTRAINT `hgjcontract_ibfk_1` FOREIGN KEY (`contempid`) REFERENCES `contemp` (`id`),
  CONSTRAINT `hgjcontract_ibfk_2` FOREIGN KEY (`subempid`) REFERENCES `employee` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of hgjcontract
-- ----------------------------

-- ----------------------------
-- Table structure for item
-- ----------------------------
DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '工程量id',
  `projectid` int(11) DEFAULT NULL COMMENT ' 项目名称id',
  `item` varchar(255) DEFAULT NULL COMMENT ' 工程量名称',
  PRIMARY KEY (`id`),
  KEY `projectid` (`projectid`),
  CONSTRAINT `item_ibfk_1` FOREIGN KEY (`projectid`) REFERENCES `project` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of item
-- ----------------------------
INSERT INTO `item` VALUES ('1', '1', '航道巡查');
INSERT INTO `item` VALUES ('2', '1', '航道测量');
INSERT INTO `item` VALUES ('3', '1', '航道疏浚');
INSERT INTO `item` VALUES ('4', '1', '水文观测及咨询');
INSERT INTO `item` VALUES ('5', '1', '养护补给');
INSERT INTO `item` VALUES ('6', '1', '仪器检测费');
INSERT INTO `item` VALUES ('7', '2', '航标巡查');
INSERT INTO `item` VALUES ('8', '2', '航标养护及维修');
INSERT INTO `item` VALUES ('9', '2', '器材购置');
INSERT INTO `item` VALUES ('10', '2', '黑中957-893km遥测遥控航标安装');
INSERT INTO `item` VALUES ('11', '3', '船体养护');
INSERT INTO `item` VALUES ('12', '3', '机电设备维修');
INSERT INTO `item` VALUES ('13', '3', '船舶检验');
INSERT INTO `item` VALUES ('14', '3', '维修配件及材料');
INSERT INTO `item` VALUES ('15', '3', '安全与通讯设备保养');
INSERT INTO `item` VALUES ('16', '3', '卫星天线');
INSERT INTO `item` VALUES ('17', '3', '水处理');
INSERT INTO `item` VALUES ('18', '4', '房屋维护-日常维护');
INSERT INTO `item` VALUES ('19', '4', '房屋维护-肇兴站房维修改造');
INSERT INTO `item` VALUES ('20', '4', '房屋维护-逊克站房维修改造');

-- ----------------------------
-- Table structure for project
-- ----------------------------
DROP TABLE IF EXISTS `project`;
CREATE TABLE `project` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '项目名称id',
  `categoryid` int(11) DEFAULT NULL COMMENT '工程名称id',
  `project` varchar(255) DEFAULT NULL COMMENT '项目名称',
  PRIMARY KEY (`id`),
  KEY `categoryid` (`categoryid`),
  CONSTRAINT `project_ibfk_1` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of project
-- ----------------------------
INSERT INTO `project` VALUES ('1', '1', '航道养护');
INSERT INTO `project` VALUES ('2', '1', '航标养护');
INSERT INTO `project` VALUES ('3', '1', '航艇养护');
INSERT INTO `project` VALUES ('4', '1', '站场养护');
INSERT INTO `project` VALUES ('5', '1', '其他');
INSERT INTO `project` VALUES ('6', '2', '航道养护');
INSERT INTO `project` VALUES ('7', '2', '航标养护');
INSERT INTO `project` VALUES ('8', '2', '航艇养护');
INSERT INTO `project` VALUES ('9', '2', '站场养护');
INSERT INTO `project` VALUES ('10', '3', '航道养护');
INSERT INTO `project` VALUES ('11', '3', '航标养护');
INSERT INTO `project` VALUES ('12', '3', '航艇养护');
INSERT INTO `project` VALUES ('13', '3', '站场养护');
INSERT INTO `project` VALUES ('14', '4', '航道养护');
INSERT INTO `project` VALUES ('15', '4', '航标养护');
INSERT INTO `project` VALUES ('16', '4', '航艇养护');
INSERT INTO `project` VALUES ('17', '4', '站场养护');

-- ----------------------------
-- Table structure for regularload
-- ----------------------------
DROP TABLE IF EXISTS `regularload`;
CREATE TABLE `regularload` (
  `id` varchar(255) NOT NULL,
  `itemid` int(11) DEFAULT NULL,
  `work` decimal(10,2) DEFAULT '0.00',
  `expense` decimal(10,2) DEFAULT '0.00',
  `year` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `itemid` (`itemid`),
  CONSTRAINT `regularload_ibfk_1` FOREIGN KEY (`itemid`) REFERENCES `item` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of regularload
-- ----------------------------

-- ----------------------------
-- Table structure for signaturedetail
-- ----------------------------
DROP TABLE IF EXISTS `signaturedetail`;
CREATE TABLE `signaturedetail` (
  `date` datetime NOT NULL COMMENT '签字的日期',
  `empid` int(11) NOT NULL COMMENT '签字的人员id',
  `conid` varchar(255) NOT NULL COMMENT '签字的会签单表的编号',
  `result` int(11) NOT NULL COMMENT '签字结果(-1拒绝，0未知,1同意)',
  `remark` varchar(255) NOT NULL COMMENT '签字的备注信息',
  `updatecount` int(11) DEFAULT NULL,
  PRIMARY KEY (`date`,`empid`,`conid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of signaturedetail
-- ----------------------------
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:37:24', '2', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:37:37', '1', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:40:01', '3', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:40:29', '4', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:40:48', '5', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:41:17', '6', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:42:27', '7', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2015-12-30 23:57:53', '8', '省界201501', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:15:58', '1', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:16:00', '1', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:16:02', '1', '省界201602', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:16:04', '1', '省界201601', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:16:06', '1', '省界201600', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:28:27', '2', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:28:29', '2', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:28:31', '2', '省界201602', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:28:34', '2', '省界201601', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:28:41', '2', '省界201600', '-1', '不好', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:09', '3', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:12', '3', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:18', '3', '省界201602', '-1', '不好\r\n', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:21', '3', '省界201601', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:47', '4', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:49', '4', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:30:51', '4', '省界201601', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:17', '5', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:19', '5', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:21', '5', '省界201601', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:43', '6', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:46', '6', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:31:56', '6', '省界201601', '-1', '不好 ', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:32:20', '8', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:32:22', '8', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:32:45', '7', '省界201605', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-01-23 20:32:47', '7', '省界201604', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-22 22:19:11', '8', '省界2016013', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-22 22:36:57', '1', '省界2016013', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:39:57', '1', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:40:21', '2', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:40:46', '3', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:40:58', '4', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:41:08', '5', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:41:20', '6', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:41:48', '7', '省内2016001', '1', 'tongyi', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:41:59', '8', '省内2016001', '1', 'ty', '0');
INSERT INTO `signaturedetail` VALUES ('2016-04-23 08:44:45', '1', '省界2016006', '1', '未填', '0');

-- ----------------------------
-- Table structure for signaturelevel
-- ----------------------------
DROP TABLE IF EXISTS `signaturelevel`;
CREATE TABLE `signaturelevel` (
  `contempid` int(11) NOT NULL COMMENT '对应的会签单模版',
  `signnum` int(11) NOT NULL COMMENT '会签单中第signnum个签字人',
  `empid` int(11) DEFAULT NULL COMMENT '对应的员工empid',
  `signlevel` int(11) DEFAULT NULL COMMENT '签字的顺序号',
  `canview` int(11) DEFAULT '0',
  `candownload` int(11) DEFAULT '0',
  PRIMARY KEY (`contempid`,`signnum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of signaturelevel
-- ----------------------------
INSERT INTO `signaturelevel` VALUES ('1', '1', '1', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '2', '2', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '3', '3', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '4', '5', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '5', '6', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '6', '7', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '7', '9', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '8', '10', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '1', '1', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '2', '2', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '3', '3', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '4', '4', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '5', '5', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '6', '6', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '7', '7', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '8', '8', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '9', '9', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '10', '10', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '11', '11', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('2', '12', '12', '1', '0', '0');

-- ----------------------------
-- Table structure for signaturestatus
-- ----------------------------
DROP TABLE IF EXISTS `signaturestatus`;
CREATE TABLE `signaturestatus` (
  `id` varchar(255) NOT NULL COMMENT '时间',
  `conid` varchar(255) NOT NULL COMMENT '提交签单的编号',
  `result1` int(11) NOT NULL,
  `result2` int(11) NOT NULL,
  `result3` int(11) NOT NULL,
  `result4` int(11) NOT NULL,
  `result5` int(11) NOT NULL,
  `result6` int(11) NOT NULL,
  `result7` int(11) NOT NULL,
  `result8` int(11) NOT NULL,
  `result9` int(11) NOT NULL,
  `result10` int(11) NOT NULL,
  `result11` int(11) NOT NULL,
  `result12` int(11) NOT NULL,
  `totalresult` int(11) NOT NULL,
  `agreecount` int(11) NOT NULL DEFAULT '0',
  `refusecount` int(11) NOT NULL,
  `currlevel` int(11) NOT NULL COMMENT '当前签字',
  `maxlevel` int(11) NOT NULL COMMENT '完成所需节点',
  `updatecount` int(11) DEFAULT '0',
  `isdownload` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of signaturestatus
-- ----------------------------
INSERT INTO `signaturestatus` VALUES ('2015-12-30 23:36:25', '省界201501', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '9', '3', '0', '1');
INSERT INTO `signaturestatus` VALUES ('2016-01-06 23:14:31', '省界201600', '1', '-1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '-1', '0', '0', '5', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-01-20 22:23:00', '省界201601', '1', '1', '1', '1', '1', '-1', '0', '0', '0', '0', '0', '0', '-1', '0', '0', '15', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-01-20 22:23:52', '省界201602', '1', '1', '-1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '-1', '0', '0', '6', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-01-22 23:48:25', '省界201604', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '21', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-01-23 20:15:48', '省界201605', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '21', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 15:56:20', '省界2016006', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '4', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 16:28:58', '省界2016007', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 16:48:07', '省界2016008', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 16:53:07', '省界2016009', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 16:55:32', '省界2016010', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 17:03:07', '省界2016011', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 17:07:03', '省界2016012', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-22 17:16:54', '省界2016013', '1', '0', '0', '0', '0', '0', '0', '1', '0', '0', '0', '0', '0', '0', '0', '7', '1', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-04-23 08:38:18', '省内2016001', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '25', '1', '0', '0');

-- ----------------------------
-- Table structure for workload
-- ----------------------------
DROP TABLE IF EXISTS `workload`;
CREATE TABLE `workload` (
  `id` varchar(255) NOT NULL,
  `contractid` varchar(255) DEFAULT NULL,
  `itemid` int(11) DEFAULT NULL,
  `work` decimal(10,2) DEFAULT NULL,
  `expense` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of workload
-- ----------------------------
DROP TRIGGER IF EXISTS `insert_signature_leve`;
DELIMITER ;;
CREATE TRIGGER `insert_signature_leve` AFTER INSERT ON `contemp` FOR EACH ROW BEGIN
    if new.type=10 or new.type=11 then 
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '1', new.signid1, new.signlevel1, new.canview1, new.candownload1);
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '2', new.signid2, new.signlevel2, new.canview2, new.candownload2);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '3', new.signid3, new.signlevel3, new.canview3, new.candownload3);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '4', new.signid4, new.signlevel4, new.canview4, new.candownload4);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '5', new.signid5, new.signlevel5, new.canview5, new.candownload5);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '6', new.signid6, new.signlevel6, new.canview6, new.candownload6);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '7', new.signid7, new.signlevel7, new.canview7, new.candownload7);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '8', new.signid8, new.signlevel8, new.canview8, new.candownload8);
    end if;
 if new.type=20 then 
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '1', new.signid1, new.signlevel1, new.canview1, new.candownload1);
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '2', new.signid2, new.signlevel2, new.canview2, new.candownload2);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '3', new.signid3, new.signlevel3, new.canview3, new.candownload3);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '4', new.signid4, new.signlevel4, new.canview4, new.candownload4);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '5', new.signid5, new.signlevel5, new.canview5, new.candownload5);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '6', new.signid6, new.signlevel6, new.canview6, new.candownload6);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '7', new.signid7, new.signlevel7, new.canview7, new.candownload7);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '8', new.signid8, new.signlevel8, new.canview8, new.candownload8);
INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '9', new.signid9, new.signlevel9, new.canview9, new.candownload9);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '10', new.signid10, new.signlevel10, new.canview10, new.candownload10);
    end if;
 if new.type=21 then 
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '1', new.signid1, new.signlevel1, new.canview1, new.candownload1);
              INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '2', new.signid2, new.signlevel2, new.canview2, new.candownload2);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '3', new.signid3, new.signlevel3, new.canview3, new.candownload3);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '4', new.signid4, new.signlevel4, new.canview4, new.candownload4);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '5', new.signid5, new.signlevel5, new.canview5, new.candownload5);
	INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '6', new.signid6, new.signlevel6, new.canview6, new.candownload6);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '7', new.signid7, new.signlevel7, new.canview7, new.candownload7);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '8', new.signid8, new.signlevel8, new.canview8, new.candownload8);
INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '9', new.signid9, new.signlevel9, new.canview9, new.candownload9);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '10', new.signid10, new.signlevel10, new.canview10, new.candownload10);
INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '11', new.signid11, new.signlevel11, new.canview11, new.candownload11);
    INSERT INTO `signaturelevel` (`contempid`, `signnum`, `empid`, `signlevel`, `canview`, `candownload`)
        VALUES (new.id, '12', new.signid12, new.signlevel12, new.canview12, new.candownload12);
    end if;
END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `update_signature_level`;
DELIMITER ;;
CREATE TRIGGER `update_signature_level` AFTER UPDATE ON `contemp` FOR EACH ROW BEGIN
 if new.type=10 or new.type=11 then
	UPDATE `signaturelevel` 
	SET `empid` = new.signid1, `signlevel` = new.signlevel1, `canview` = new.canview1, `candownload` = new.candownload1
	WHERE(`contempid` = new.id and `signnum` = 1);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid2, `signlevel` = new.signlevel2, `canview` = new.canview2, `candownload` = new.candownload2
	WHERE(`contempid` = new.id and `signnum` = 2);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid3, `signlevel` = new.signlevel3, `canview` = new.canview3, `candownload` = new.candownload3
	WHERE(`contempid` = new.id and `signnum` = 3);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid4, `signlevel` = new.signlevel4, `canview` = new.canview4, `candownload` = new.candownload4
	WHERE(`contempid` = new.id and `signnum` = 4);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid5, `signlevel` = new.signlevel5, `canview` = new.canview5, `candownload` = new.candownload5
	WHERE(`contempid` = new.id and `signnum` = 5);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid6, `signlevel` = new.signlevel6, `canview` = new.canview6, `candownload` = new.candownload6
	WHERE(`contempid` = new.id and `signnum` = 6);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid7, `signlevel` = new.signlevel7, `canview` = new.canview7, `candownload` = new.candownload7
	WHERE(`contempid` = new.id and `signnum` = 7);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid8, `signlevel` = new.signlevel8, `canview` = new.canview8, `candownload` = new.candownload8
	WHERE(`contempid` = new.id and `signnum` = 8);
end if;

 if new.type=20 then
	UPDATE `signaturelevel` 
	SET `empid` = new.signid1, `signlevel` = new.signlevel1, `canview` = new.canview1, `candownload` = new.candownload1
	WHERE(`contempid` = new.id and `signnum` = 1);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid2, `signlevel` = new.signlevel2, `canview` = new.canview2, `candownload` = new.candownload2
	WHERE(`contempid` = new.id and `signnum` = 2);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid3, `signlevel` = new.signlevel3, `canview` = new.canview3, `candownload` = new.candownload3
	WHERE(`contempid` = new.id and `signnum` = 3);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid4, `signlevel` = new.signlevel4, `canview` = new.canview4, `candownload` = new.candownload4
	WHERE(`contempid` = new.id and `signnum` = 4);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid5, `signlevel` = new.signlevel5, `canview` = new.canview5, `candownload` = new.candownload5
	WHERE(`contempid` = new.id and `signnum` = 5);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid6, `signlevel` = new.signlevel6, `canview` = new.canview6, `candownload` = new.candownload6
	WHERE(`contempid` = new.id and `signnum` = 6);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid7, `signlevel` = new.signlevel7, `canview` = new.canview7, `candownload` = new.candownload7
	WHERE(`contempid` = new.id and `signnum` = 7);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid8, `signlevel` = new.signlevel8, `canview` = new.canview8, `candownload` = new.candownload8
	WHERE(`contempid` = new.id and `signnum` = 8);

              UPDATE `signaturelevel` 
	SET `empid` = new.signid9, `signlevel` = new.signlevel9, `canview` = new.canview9, `candownload` = new.candownload9
	WHERE(`contempid` = new.id and `signnum` = 9);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid10, `signlevel` = new.signlevel10, `canview` = new.canview10, `candownload` = new.candownload10
	WHERE(`contempid` = new.id and `signnum` = 10);
end if;

if new.type=21 then
	UPDATE `signaturelevel` 
	SET `empid` = new.signid1, `signlevel` = new.signlevel1, `canview` = new.canview1, `candownload` = new.candownload1
	WHERE(`contempid` = new.id and `signnum` = 1);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid2, `signlevel` = new.signlevel2, `canview` = new.canview2, `candownload` = new.candownload2
	WHERE(`contempid` = new.id and `signnum` = 2);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid3, `signlevel` = new.signlevel3, `canview` = new.canview3, `candownload` = new.candownload3
	WHERE(`contempid` = new.id and `signnum` = 3);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid4, `signlevel` = new.signlevel4, `canview` = new.canview4, `candownload` = new.candownload4
	WHERE(`contempid` = new.id and `signnum` = 4);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid5, `signlevel` = new.signlevel5, `canview` = new.canview5, `candownload` = new.candownload5
	WHERE(`contempid` = new.id and `signnum` = 5);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid6, `signlevel` = new.signlevel6, `canview` = new.canview6, `candownload` = new.candownload6
	WHERE(`contempid` = new.id and `signnum` = 6);

	UPDATE `signaturelevel` 
	SET `empid` = new.signid7, `signlevel` = new.signlevel7, `canview` = new.canview7, `candownload` = new.candownload7
	WHERE(`contempid` = new.id and `signnum` = 7);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid8, `signlevel` = new.signlevel8, `canview` = new.canview8, `candownload` = new.candownload8
	WHERE(`contempid` = new.id and `signnum` = 8);

              UPDATE `signaturelevel` 
	SET `empid` = new.signid9, `signlevel` = new.signlevel9, `canview` = new.canview9, `candownload` = new.candownload9
	WHERE(`contempid` = new.id and `signnum` = 9);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid10, `signlevel` = new.signlevel10, `canview` = new.canview10, `candownload` = new.candownload10
	WHERE(`contempid` = new.id and `signnum` = 10);
              UPDATE `signaturelevel` 
	SET `empid` = new.signid11, `signlevel` = new.signlevel11, `canview` = new.canview11, `candownload` = new.candownload11
	WHERE(`contempid` = new.id and `signnum` = 11);
	
	UPDATE `signaturelevel` 
	SET `empid` = new.signid12, `signlevel` = new.signlevel12, `canview` = new.canview12, `candownload` = new.candownload12
	WHERE(`contempid` = new.id and `signnum` = 12);
end if;

END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `set_conidcategory`;
DELIMITER ;;
CREATE TRIGGER `set_conidcategory` BEFORE UPDATE ON `department` FOR EACH ROW BEGIN
    if (old.canboundary = 0 and new.canboundary = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 1);
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 2);
    elseif (old.canboundary = 1 and new.canboundary = 0) then 
        DELETE from `conidcategory` 
        WHERE(`departmentid` = new.id and `categoryid` = 1);
        DELETE from `conidcategory` 
        WHERE(`departmentid` = new.id and `categoryid` = 2);
    end if;

    if (old.caninland = 0 and new.caninland = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 3);
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 4);
    elseif (old.caninland = 1 and new.caninland = 0) then 
        DELETE from `conidcategory`
        WHERE(`departmentid` = new.id and `categoryid` = 3);
        DELETE from `conidcategory`
        WHERE(`departmentid` = new.id and `categoryid` = 4);
    end if;
          
 END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `insert_signature_status`;
DELIMITER ;;
CREATE TRIGGER `insert_signature_status` AFTER INSERT ON `hgjcontract` FOR EACH ROW BEGIN
   if new.contempid=10 or new.contempid=11 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', (SELECT c.signlevel8 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;

if new.contempid=20 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `result9`,`result10`,  `totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0',  '0',  '0', '0', '0', '0', '1', (SELECT c.signlevel10 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;
if new.contempid=21 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `result9`,`result10`, `result11`,`result12`,`totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0',  '0', '0', '0',  '0', '0', '0', '0', '1', (SELECT c.signlevel10 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;

END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `update_signature_status`;
DELIMITER ;;
CREATE TRIGGER `update_signature_status` AFTER UPDATE ON `hgjcontract` FOR EACH ROW BEGIN
 if new.contempid=10 or new.contempid=11 then
            UPDATE `signaturestatus`
            set `result1` = '0', `result2` = '0', `result3` = '0', `result4` = '0', `result5` = '0', `result6` = '0', `result7` = '0', `result8` = '0', `totalresult` = '0', `agreecount` = '0', `refusecount` = '0', `currlevel` = '1', `updatecount` = `updatecount` + 1
            WHERE (conid = new.id);
end if;

if new.contempid=20 then
            UPDATE `signaturestatus`
            set `result1` = '0', `result2` = '0', `result3` = '0', `result4` = '0', `result5` = '0', `result6` = '0', `result7` = '0', `result8` = '0',`result9` = '0',`result10` = '0', `totalresult` = '0', `agreecount` = '0', `refusecount` = '0', `currlevel` = '1', `updatecount` = `updatecount` + 1
            WHERE (conid = new.id);
end if;

if new.contempid=21 then
            UPDATE `signaturestatus`
            set `result1` = '0', `result2` = '0', `result3` = '0', `result4` = '0', `result5` = '0', `result6` = '0', `result7` = '0', `result8` = '0',`result9` = '0',`result10` = '0', `result11` = '0',`result12` = '0',`totalresult` = '0', `agreecount` = '0', `refusecount` = '0', `currlevel` = '1', `updatecount` = `updatecount` + 1
            WHERE (conid = new.id);
end if;
        END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `modify_signature_status`;
DELIMITER ;;
CREATE TRIGGER `modify_signature_status` BEFORE INSERT ON `signaturedetail` FOR EACH ROW BEGIN
            set @type = (SELECT `contempid` FROM `hdjcontract` WHERE (conid = new.conid));
            set new.updatecount = (SELECT `updatecount` FROM `signaturestatus` WHERE (conid = new.conid));
if @type=10 or @type=11 then
            UPDATE `signaturestatus`
            SET result1 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid1 = e.id)));
        
            UPDATE `signaturestatus`
            SET result2 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid2 = e.id)));
            
            UPDATE `signaturestatus`
            SET result3 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid3 = e.id)));
            
            UPDATE `signaturestatus`
            SET result4 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid4 = e.id)));
            
            UPDATE `signaturestatus`
            SET result5 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid5 = e.id)));
            
            UPDATE `signaturestatus`
            SET result6 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid6 = e.id)));
            
            UPDATE `signaturestatus`
            SET result7 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid7 = e.id)));
        
            UPDATE `signaturestatus`
            SET result8 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid8 = e.id)));
    end if; 

if @type=20 then
            UPDATE `signaturestatus`
            SET result1 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid1 = e.id)));
        
            UPDATE `signaturestatus`
            SET result2 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid2 = e.id)));
            
            UPDATE `signaturestatus`
            SET result3 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid3 = e.id)));
            
            UPDATE `signaturestatus`
            SET result4 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid4 = e.id)));
            
            UPDATE `signaturestatus`
            SET result5 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid5 = e.id)));
            
            UPDATE `signaturestatus`
            SET result6 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid6 = e.id)));
            
            UPDATE `signaturestatus`
            SET result7 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid7 = e.id)));
        
            UPDATE `signaturestatus`
            SET result8 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid8 = e.id)));
           UPDATE `signaturestatus`
            SET result9 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid9 = e.id)));
           UPDATE `signaturestatus`
            SET result10 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid10 = e.id)));
    end if;       

if @type=21 then
            UPDATE `signaturestatus`
            SET result1 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid1 = e.id)));
        
            UPDATE `signaturestatus`
            SET result2 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid2 = e.id)));
            
            UPDATE `signaturestatus`
            SET result3 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid3 = e.id)));
            
            UPDATE `signaturestatus`
            SET result4 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid4 = e.id)));
            
            UPDATE `signaturestatus`
            SET result5 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid5 = e.id)));
            
            UPDATE `signaturestatus`
            SET result6 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid6 = e.id)));
            
            UPDATE `signaturestatus`
            SET result7 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid7 = e.id)));
        
            UPDATE `signaturestatus`
            SET result8 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid8 = e.id)));
           UPDATE `signaturestatus`
            SET result9 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid9 = e.id)));
           UPDATE `signaturestatus`
            SET result10 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid10 = e.id)));
           UPDATE `signaturestatus`
            SET result11 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid11 = e.id)));
           UPDATE `signaturestatus`
            SET result12 = new.result 
            WHERE (signaturestatus.conid = new.conid 
               and new.empid = (SELECT e.id FROM `hdjcontract` h, contemp c, employee e WHERE (h.id = signaturestatus.conid and h.contempid = c.id and c.signid12 = e.id)));
    end if;       
        END
;;
DELIMITER ;
