/*
Navicat MySQL Data Transfer

Source Server         : localhost_3333
Source Server Version : 50529
Source Host           : localhost:3333
Source Database       : hdjsignaturedb

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2016-05-11 18:32:56
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of conidcategory
-- ----------------------------
INSERT INTO `conidcategory` VALUES ('1', '1', '1');
INSERT INTO `conidcategory` VALUES ('2', '1', '2');
INSERT INTO `conidcategory` VALUES ('3', '1', '3');
INSERT INTO `conidcategory` VALUES ('4', '1', '4');
INSERT INTO `conidcategory` VALUES ('5', '6', '1');
INSERT INTO `conidcategory` VALUES ('6', '6', '2');
INSERT INTO `conidcategory` VALUES ('7', '6', '3');
INSERT INTO `conidcategory` VALUES ('8', '6', '4');

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
  `signid4` int(11) DEFAULT '0' COMMENT '签字人4的员工号',
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of contemp
-- ----------------------------
INSERT INTO `contemp` VALUES ('1', '2016-05-11 14:53:50', '内河航道例行养护工程费用使用审批表', '10', '工程名称', '项目名称', '主要工程量', '本次申请资金额度（元）', '累计申请资金额度（元）', '主办单位审核意见', '航道局主办部门负责人（签字）', '航道局主管负责人（签字）', '航道局负责人（签字）', '监理单位负责人（签字）', '航道处主办人员（签字）', '航道处主管负责人（签字）', '财审处负责人（签字）', '主管局长（签字）', null, null, null, null, '2', '3', '4', '1', '8', '9', '12', '15', '0', '0', '0', null, '1', '2', '3', '-1', '4', '5', '6', '7', null, null, null, null, '1', '1', '1', '0', '1', '1', '1', '1', null, null, null, null, '1', '1', '1', '0', '1', '1', '1', '1', null, null, null, null);
INSERT INTO `contemp` VALUES ('2', '2016-05-11 16:57:25', '内河航道专项养护工程费用使用审批表', '11', '工程名称', '项目名称', '主要工程量', '本次申请资金额度（元）', '累计申请资金额度（元）', '主办单位审核意见', '航道局主办部门负责人（签字）', '航道局主管负责人（签字）', '航道局负责人（签字）', '监理单位负责人（签字）', '航道处主办人员（签字）', '航道处主管负责人（签字）', '财审处负责人（签字）', '主管局长', null, null, null, null, '2', '3', '4', '6', '8', '9', '12', '15', '0', '0', '0', null, '1', '2', '3', '-1', '5', '6', '7', '8', null, null, null, null, '1', '1', '1', '1', '1', '1', '1', '1', null, null, null, null, '1', '1', '1', '1', '1', '1', '1', '1', null, null, null, null);
INSERT INTO `contemp` VALUES ('3', '2016-05-11 17:02:30', '界河航道例行养护工程拨款会签审批单', '20', '工程名称', '项目名称', '主要工程量', '本次申请资金额度（元）', '累计申请资金额度（元）', '省航道局审核意见', '实施单位项目负责人（签字）', '实施单位负责人（签字）', '省航道局养护负责人（签字）', '省航道局主管局长（签字）', '省航道局局长（签字）', '养护项目指挥部审核人员（签字）', '养护项目指挥部负责人（签字）', '航道管理处负责人（签字）', '财务审计处负责人（签字）', '主管局长', null, null, '16', '17', '2', '3', '4', '14', '15', '5', '12', '11', '0', null, '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', null, null, '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', null, null, '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', null, null);
INSERT INTO `contemp` VALUES ('4', '2016-05-11 18:08:06', '界河航道专项养护工程拨款会签审批表', '21', '工程名称', '项目名称', '主要工程量', '本次申请资金额度（元）', '累计申请资金额度（元）', null, '实施单位项目负责人（签字）', '实施单位负责人（签字）', '省航道局养护负责人（签字）', '主管局长（签字）', '局长（签字）', '监理单位总监（签字）', '监理单位负责人（签字）', '指挥部养护项目审核人员（签字）', '指挥部养护项目负责人（签字）', '航道管理处负责人（签字）', '财务审计处负责人（签字）', '主管局长（签字）', '16', '17', '2', '3', '4', '6', '7', '14', '15', '8', '12', '5', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1');

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
  `highdepid` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`) USING BTREE,
  KEY `highdepid` (`highdepid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('1', '黑龙江省航道局', '省', '1', '1', '1');
INSERT INTO `department` VALUES ('2', '监理单位', '监', '0', '0', '0');
INSERT INTO `department` VALUES ('3', '航道处', '处', '0', '0', '0');
INSERT INTO `department` VALUES ('4', '财务处', '财', '0', '0', '0');
INSERT INTO `department` VALUES ('5', '指挥部', '指', '1', '1', '0');
INSERT INTO `department` VALUES ('6', '项目实施单位', '实', '1', '1', '0');

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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('1', '胡浩', '负责人', '1', '1', '0', '0', '1', 'huhao', 'huhao');
INSERT INTO `employee` VALUES ('2', '成坚', '负责人', '1', '1', '1', '1', '1', 'chengjian', 'chengjian');
INSERT INTO `employee` VALUES ('3', '李香元', '负责人', '1', '1', '1', '1', '1', 'lixiangyuan', 'lixiangyuan');
INSERT INTO `employee` VALUES ('4', '张三', '负责人', '1', '1', '1', '1', '1', 'zhangsan', 'zhangsan');
INSERT INTO `employee` VALUES ('5', '李四', '负责人', '1', '1', '1', '1', '1', 'lisi', 'lisi');
INSERT INTO `employee` VALUES ('6', '石啸', '负责人', '2', '1', '1', '1', '1', 'shixiao', 'shixiao');
INSERT INTO `employee` VALUES ('7', '王二', '负责人', '2', '1', '1', '1', '1', 'wanger', 'wanger');
INSERT INTO `employee` VALUES ('8', '赵强', '负责人', '3', '1', '1', '1', '1', 'zhaoqiang', 'zhaoqiang');
INSERT INTO `employee` VALUES ('9', '许伟', '负责人', '3', '1', '1', '1', '1', 'xuwei', 'xuwei');
INSERT INTO `employee` VALUES ('10', '王盼盼', '负责人', '3', '1', '1', '1', '1', 'wangpanpan', 'wangpanpan');
INSERT INTO `employee` VALUES ('11', '吴佳怡', '负责人', '3', '1', '1', '1', '1', 'wujiayi', 'wujiayi');
INSERT INTO `employee` VALUES ('12', '凌煊峰', '负责人', '4', '1', '1', '1', '1', 'lingxuanfeng', 'lingxuanfeng');
INSERT INTO `employee` VALUES ('13', '张琦', '负责人', '4', '1', '1', '1', '1', 'zhangqi', 'zhangqi');
INSERT INTO `employee` VALUES ('14', '王勇', '负责人', '5', '1', '1', '1', '1', 'wangyong', 'wangyong');
INSERT INTO `employee` VALUES ('15', '王克朝', '负责人', '5', '1', '1', '1', '1', 'wangkechao', 'wangkechao');
INSERT INTO `employee` VALUES ('16', '李一', '负责人', '6', '1', '1', '1', '1', 'liyi', 'liyi');
INSERT INTO `employee` VALUES ('17', '宋亮', '负责人', '6', '1', '1', '1', '1', 'songliang', 'songliang');

-- ----------------------------
-- Table structure for hdjcontract
-- ----------------------------
DROP TABLE IF EXISTS `hdjcontract`;
CREATE TABLE `hdjcontract` (
  `id` varchar(255) NOT NULL COMMENT '提交的会签单的编号',
  `name` varchar(255) NOT NULL COMMENT '提交的会签单的名称',
  `contempid` int(11) NOT NULL COMMENT '当前会签单所使用的模版类',
  `columndata1` varchar(255) NOT NULL,
  `columndata2` varchar(255) NOT NULL,
  `columndata3` varchar(255) NOT NULL,
  `columndata4` varchar(255) NOT NULL,
  `columndata5` varchar(255) NOT NULL,
  `columndata6` varchar(255) DEFAULT NULL,
  `subempid` int(11) NOT NULL COMMENT '申请人的id',
  `submitdate` datetime DEFAULT NULL COMMENT ' 提交时间',
  PRIMARY KEY (`id`),
  KEY `contempid` (`contempid`),
  KEY `subempid` (`subempid`),
  CONSTRAINT `hdjcontract_ibfk_1` FOREIGN KEY (`contempid`) REFERENCES `contemp` (`id`),
  CONSTRAINT `hdjcontract_ibfk_2` FOREIGN KEY (`subempid`) REFERENCES `employee` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of hdjcontract
-- ----------------------------
INSERT INTO `hdjcontract` VALUES ('省内专2016001', '内河航道专项养护工程费用使用审批表', '2', '内河航道专项养护工程', '航道养护', '航道巡查   工作量 : 1000   投资额 : 1000\r\n', '1000    壹仟元', '1000    壹仟元', '同意', '2', '2016-05-11 18:08:40');
INSERT INTO `hdjcontract` VALUES ('省内例2016001', '内河航道例行养护工程费用使用审批表', '1', '内河航道例行养护工程', '航道养护', ' 航道巡查   工作量 : 100   投资额 : 1000\r\n', '1000    壹仟元', '1000    壹仟元', '同意', '2', '2016-05-11 14:55:14');
INSERT INTO `hdjcontract` VALUES ('省界专2016001', '界河航道专项养护工程拨款会签审批表', '4', '界河航道专项养护工程', '航道养护', '航道巡查   工作量 : 100   投资额 : 1000\r\n', '1000    壹仟元', '1000    壹仟元', null, '2', '2016-05-11 18:16:23');
INSERT INTO `hdjcontract` VALUES ('省界例2016001', '界河航道例行养护工程拨款会签审批单', '3', '界河航道例行养护工程', '航道养护', '航道巡查   工作量 : 100   投资额 : 1000\r\n', '1000    壹仟元', '1000    壹仟元', '同意该申请', '2', '2016-05-11 18:12:19');

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
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

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
INSERT INTO `item` VALUES ('21', '5', '航标巡查');
INSERT INTO `item` VALUES ('22', '5', '航标养护及维修');
INSERT INTO `item` VALUES ('23', '5', '器材购置');
INSERT INTO `item` VALUES ('24', '5', '黑中957-893km遥测遥控航标安装');
INSERT INTO `item` VALUES ('25', '6', '航道巡查');
INSERT INTO `item` VALUES ('26', '6', '航道测量');
INSERT INTO `item` VALUES ('27', '6', '航道疏浚');
INSERT INTO `item` VALUES ('28', '6', '水文观测及咨询');
INSERT INTO `item` VALUES ('29', '10', ' 航道巡查');
INSERT INTO `item` VALUES ('30', '14', ' 航道巡查');

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
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:55:59', '2', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:56:17', '3', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:56:29', '4', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:56:49', '8', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:57:03', '9', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:57:14', '12', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 14:57:25', '15', '省内例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:08:48', '2', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:09:02', '3', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:09:18', '4', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:09:33', '6', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:09:52', '8', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:10:05', '9', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:10:16', '12', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:10:26', '15', '省内专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:12:34', '16', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:12:46', '17', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:13:12', '2', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:13:25', '3', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:13:36', '4', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:13:46', '14', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:13:58', '15', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:14:10', '5', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:14:22', '12', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:14:39', '11', '省界例2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:22:53', '16', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:23:07', '17', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:23:18', '2', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:23:29', '3', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:23:43', '4', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:23:56', '6', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:24:50', '7', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:30:05', '14', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:30:16', '15', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:30:28', '8', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:30:42', '12', '省界专2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-05-11 18:30:52', '5', '省界专2016001', '1', '未填', '0');

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
INSERT INTO `signaturelevel` VALUES ('1', '1', '2', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '2', '3', '2', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '3', '4', '3', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '4', '1', '-1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '5', '8', '4', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '6', '9', '5', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '7', '12', '6', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '8', '15', '7', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '1', '2', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '2', '3', '2', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '3', '4', '3', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '4', '6', '-1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '5', '8', '5', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '6', '9', '6', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '7', '12', '7', '1', '1');
INSERT INTO `signaturelevel` VALUES ('2', '8', '15', '8', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '1', '16', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '2', '17', '2', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '3', '2', '3', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '4', '3', '4', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '5', '4', '5', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '6', '14', '6', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '7', '15', '7', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '8', '5', '8', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '9', '12', '9', '1', '1');
INSERT INTO `signaturelevel` VALUES ('3', '10', '11', '10', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '1', '16', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '2', '17', '2', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '3', '2', '3', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '4', '3', '4', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '5', '4', '5', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '6', '6', '6', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '7', '7', '7', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '8', '14', '8', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '9', '15', '9', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '10', '8', '10', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '11', '12', '11', '1', '1');
INSERT INTO `signaturelevel` VALUES ('4', '12', '5', '12', '1', '1');

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
  `result9` int(11) DEFAULT '0',
  `result10` int(11) DEFAULT '0',
  `result11` int(11) DEFAULT '0',
  `result12` int(11) DEFAULT '0',
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
INSERT INTO `signaturestatus` VALUES ('2016-05-11 14:55:14', '省内例2016001', '1', '1', '1', '0', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '8', '7', '0', '1');
INSERT INTO `signaturestatus` VALUES ('2016-05-11 18:08:40', '省内专2016001', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '1', '0', '0', '9', '8', '0', '1');
INSERT INTO `signaturestatus` VALUES ('2016-05-11 18:12:19', '省界例2016001', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '1', '0', '0', '11', '10', '0', '1');
INSERT INTO `signaturestatus` VALUES ('2016-05-11 18:16:23', '省界专2016001', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '13', '12', '0', '1');

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
INSERT INTO `workload` VALUES ('省内专20160011', '省内专2016001', '1', '1000.00', '1000.00');
INSERT INTO `workload` VALUES ('省内例201600129', '省内例2016001', '29', '100.00', '1000.00');
INSERT INTO `workload` VALUES ('省界专20160011', '省界专2016001', '1', '100.00', '1000.00');
INSERT INTO `workload` VALUES ('省界例20160011', '省界例2016001', '1', '100.00', '1000.00');
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
DROP TRIGGER IF EXISTS `insert_conidcategory`;
DELIMITER ;;
CREATE TRIGGER `insert_conidcategory` AFTER INSERT ON `department` FOR EACH ROW BEGIN
   if new.canboundary=1 then
            INSERT INTO `conidcategory` (`departmentid`, `categoryid`) 
            VALUES (new.id, '1');
           INSERT INTO `conidcategory` (`departmentid`, `categoryid`) 
            VALUES (new.id, '2');
   end if;

 if new.caninland=1 then
              INSERT INTO `conidcategory` (`departmentid`, `categoryid`) 
            VALUES (new.id, '3');
           INSERT INTO `conidcategory` (`departmentid`, `categoryid`) 
            VALUES (new.id, '4');
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
CREATE TRIGGER `insert_signature_status` AFTER INSERT ON `hdjcontract` FOR EACH ROW BEGIN
   set @type = (SELECT `type` FROM `contemp` WHERE (id = new.contempid));
   if @type=10 or @type=11 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', (SELECT c.signlevel8 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;

if @type=20 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `result9`,`result10`,  `totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0',  '0',  '0', '0', '0', '0', '1', (SELECT c.signlevel10 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;
if @type=21 then
            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `result9`,`result10`, `result11`,`result12`,`totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0',  '0', '0', '0',  '0', '0', '0', '0', '1', (SELECT c.signlevel12 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');
   end if;

END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `modify_signature_status`;
DELIMITER ;;
CREATE TRIGGER `modify_signature_status` BEFORE INSERT ON `signaturedetail` FOR EACH ROW BEGIN
            set @type = (SELECT `type` FROM `contemp`,`hdjcontract` WHERE (hdjcontract.id=new.conid and contemp.id = hdjcontract.contempid));
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
DROP TRIGGER IF EXISTS `set_signature_status_totalresult`;
DELIMITER ;;
CREATE TRIGGER `set_signature_status_totalresult` BEFORE UPDATE ON `signaturestatus` FOR EACH ROW BEGIN
set @type = (SELECT `type` FROM `contemp`,`hdjcontract` WHERE (hdjcontract.id=new.conid and contemp.id = hdjcontract.contempid));
	if @type=10 then
		 if (new.result1 = '1' and new.result2 = '1' and new.result3 = '1' and new.result5 = '1' and new.result6 = '1' and new.result7 = '1' and new.result8 = '1')
			then set new.totalresult = '1';
		elseif (new.result1 = '-1' or new.result2 = '-1' or new.result3 = '-1' or new.result5 = '-1' or new.result6 = '-1' or new.result7 = '-1' or new.result8 = '-1')
			then set new.totalresult = '-1';
		end if;
              end if;
              if  @type=11 then
		 if (new.result1 = '1' and new.result2 = '1' and new.result3 = '1' and new.result4 = '1' and new.result5 = '1' and new.result6 = '1' and new.result7 = '1' and new.result8 = '1')
			then set new.totalresult = '1';
		elseif (new.result1 = '-1' or new.result2 = '-1' or new.result3 = '-1' or new.result4 = '-1' or new.result5 = '-1' or new.result6 = '-1' or new.result7 = '-1' or new.result8 = '-1')
			then set new.totalresult = '-1';
		end if;
              end if;
	if @type=20 then
		 if (new.result1 = '1' and new.result2 = '1' and new.result3 = '1' and new.result4 = '1' and new.result5 = '1' and new.result6 = '1' and new.result7 = '1' and new.result8 = '1' and new.result9 = '1' and new.result10 = '1')
			then set new.totalresult = '1';
		elseif (new.result1 = '-1' or new.result2 = '-1' or new.result3 = '-1' or new.result4 = '-1' or new.result5 = '-1' or new.result6 = '-1' or new.result7 = '-1' or new.result8 = '-1' or new.result9 = '-1' or new.result10 = '-1')
			then set new.totalresult = '-1';
		end if;
              end if;
             if @type=21 then
		 if (new.result1 = '1' and new.result2 = '1' and new.result3 = '1' and new.result4 = '1' and new.result5 = '1' and new.result6 = '1' and new.result7 = '1' and new.result8 = '1' and new.result9 = '1' and new.result10 = '1' and new.result11 = '1' and new.result12 = '1')
			then set new.totalresult = '1';
		elseif (new.result1 = '-1' or new.result2 = '-1' or new.result3 = '-1' or new.result4 = '-1' or new.result5 = '-1' or new.result6 = '-1' or new.result7 = '-1' or new.result8 = '-1' or new.result9 = '-1' or new.result10 = '-1' or new.result11 = '-1' or new.result12 = '-1')
			then set new.totalresult = '-1';
		end if;
              end if;
END
;;
DELIMITER ;
