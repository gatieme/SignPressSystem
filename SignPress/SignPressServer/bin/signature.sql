/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50529
Source Host           : localhost:3333
Source Database       : signature

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2016-04-17 20:27:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `category` varchar(255) DEFAULT NULL,
  `shortcall` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('1', '界河航道养护工程', '界');
INSERT INTO `category` VALUES ('2', '内河航道养护工程', '内');
INSERT INTO `category` VALUES ('3', '应急抢通工程', '应');
INSERT INTO `category` VALUES ('4', '例会项目工程', '例');

-- ----------------------------
-- Table structure for conidcategory
-- ----------------------------
DROP TABLE IF EXISTS `conidcategory`;
CREATE TABLE `conidcategory` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `departmentid` int(11) DEFAULT NULL,
  `categoryid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `categoryid` (`categoryid`),
  KEY `conidcategory_ibfk_1` (`departmentid`),
  CONSTRAINT `conidcategory_ibfk_1` FOREIGN KEY (`departmentid`) REFERENCES `department` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `conidcategory_ibfk_2` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of conidcategory
-- ----------------------------
INSERT INTO `conidcategory` VALUES ('1', '1', '1');
INSERT INTO `conidcategory` VALUES ('2', '1', '2');
INSERT INTO `conidcategory` VALUES ('5', '4', '1');
INSERT INTO `conidcategory` VALUES ('7', '4', '3');
INSERT INTO `conidcategory` VALUES ('8', '4', '4');
INSERT INTO `conidcategory` VALUES ('9', '3', '1');
INSERT INTO `conidcategory` VALUES ('10', '3', '2');
INSERT INTO `conidcategory` VALUES ('11', '3', '3');
INSERT INTO `conidcategory` VALUES ('12', '3', '4');
INSERT INTO `conidcategory` VALUES ('13', '2', '1');
INSERT INTO `conidcategory` VALUES ('14', '2', '2');
INSERT INTO `conidcategory` VALUES ('15', '2', '3');
INSERT INTO `conidcategory` VALUES ('17', '5', '4');
INSERT INTO `conidcategory` VALUES ('18', '5', '3');
INSERT INTO `conidcategory` VALUES ('19', '6', '3');
INSERT INTO `conidcategory` VALUES ('20', '6', '4');
INSERT INTO `conidcategory` VALUES ('21', '7', '3');
INSERT INTO `conidcategory` VALUES ('22', '7', '4');
INSERT INTO `conidcategory` VALUES ('23', '8', '1');
INSERT INTO `conidcategory` VALUES ('24', '8', '2');
INSERT INTO `conidcategory` VALUES ('25', '8', '3');
INSERT INTO `conidcategory` VALUES ('26', '8', '4');
INSERT INTO `conidcategory` VALUES ('27', '9', '1');
INSERT INTO `conidcategory` VALUES ('28', '9', '2');
INSERT INTO `conidcategory` VALUES ('29', '10', '1');
INSERT INTO `conidcategory` VALUES ('30', '10', '2');
INSERT INTO `conidcategory` VALUES ('31', '11', '1');
INSERT INTO `conidcategory` VALUES ('32', '11', '2');
INSERT INTO `conidcategory` VALUES ('33', '12', '1');
INSERT INTO `conidcategory` VALUES ('34', '12', '2');
INSERT INTO `conidcategory` VALUES ('35', '14', '1');
INSERT INTO `conidcategory` VALUES ('36', '14', '2');
INSERT INTO `conidcategory` VALUES ('37', '15', '1');
INSERT INTO `conidcategory` VALUES ('38', '15', '2');
INSERT INTO `conidcategory` VALUES ('39', '16', '1');
INSERT INTO `conidcategory` VALUES ('40', '16', '2');

-- ----------------------------
-- Table structure for contemp
-- ----------------------------
DROP TABLE IF EXISTS `contemp`;
CREATE TABLE `contemp` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '会签单模版编号',
  `createdate` datetime NOT NULL,
  `name` varchar(255) NOT NULL COMMENT '会签单模版名称',
  `column1` varchar(255) NOT NULL COMMENT '栏目1',
  `column2` varchar(255) NOT NULL COMMENT '栏目2',
  `column3` varchar(255) NOT NULL COMMENT '栏目3',
  `column4` varchar(255) NOT NULL COMMENT '栏目4',
  `column5` varchar(255) NOT NULL COMMENT '栏目5',
  `signinfo1` varchar(255) NOT NULL COMMENT '签字人1信息',
  `signinfo2` varchar(255) NOT NULL COMMENT '签字人2信息',
  `signinfo3` varchar(255) NOT NULL COMMENT '签字人3信息',
  `signinfo4` varchar(255) NOT NULL COMMENT '签字人4信息',
  `signinfo5` varchar(255) NOT NULL COMMENT '签字人5信息',
  `signinfo6` varchar(255) NOT NULL COMMENT '签字人6信息',
  `signinfo7` varchar(255) NOT NULL COMMENT '签字人7信息',
  `signinfo8` varchar(255) NOT NULL COMMENT '签字人8信息',
  `signid1` int(11) NOT NULL COMMENT '签字人1的员工号',
  `signid2` int(11) NOT NULL COMMENT '签字人2的员工号',
  `signid3` int(11) NOT NULL COMMENT '签字人3的员工号',
  `signid4` int(11) NOT NULL COMMENT '签字人4的员工号',
  `signid5` int(11) NOT NULL COMMENT '签字人5的员工号',
  `signid6` int(11) NOT NULL COMMENT '签字人6的员工号',
  `signid7` int(11) NOT NULL COMMENT '签字人7的员工号',
  `signid8` int(11) NOT NULL COMMENT '签字人8的员工号',
  `signlevel1` int(11) DEFAULT NULL COMMENT '签字人1的签字顺序',
  `signlevel2` int(11) DEFAULT NULL COMMENT '签字人2的签字顺序',
  `signlevel3` int(11) DEFAULT NULL COMMENT '签字人3的签字顺序',
  `signlevel4` int(11) DEFAULT NULL COMMENT '签字人4的签字顺序',
  `signlevel5` int(11) DEFAULT NULL COMMENT '签字人5的签字顺序',
  `signlevel6` int(11) DEFAULT NULL COMMENT '签字人6的签字顺序',
  `signlevel7` int(11) DEFAULT NULL COMMENT '签字人7的签字顺序',
  `signlevel8` int(11) DEFAULT NULL COMMENT '签字人8的签字顺序',
  `canview1` int(11) DEFAULT NULL,
  `canview2` int(11) DEFAULT NULL,
  `canview3` int(11) DEFAULT NULL,
  `canview4` int(11) DEFAULT NULL,
  `canview5` int(11) DEFAULT NULL,
  `canview6` int(11) DEFAULT NULL,
  `canview7` int(11) DEFAULT NULL,
  `canview8` int(11) DEFAULT NULL,
  `candownload1` int(11) DEFAULT NULL,
  `candownload2` int(11) DEFAULT NULL,
  `candownload3` int(11) DEFAULT NULL,
  `candownload4` int(11) DEFAULT NULL,
  `candownload5` int(11) DEFAULT NULL,
  `candownload6` int(11) DEFAULT NULL,
  `candownload7` int(11) DEFAULT NULL,
  `candownload8` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of contemp
-- ----------------------------
INSERT INTO `contemp` VALUES ('1', '2015-06-20 12:30:30', '养护及例会项目拨款会签审批单模版 ', '工程名称', '项目名称', '主要项目及工程量', '本次申请资金额度（元）', '累计申请资金额度（元）', '申请单位项目负责人', '申请单位负责人', '养护主管部门项目负责人', '养护主管部门负责人', '计划科负责人', '财务科负责人', '主 管 局 长', '局      长', '1', '2', '3', '4', '5', '6', '8', '7', '1', '1', '1', '1', '1', '1', '2', '3', '0', '0', '1', '0', '0', '0', '1', '1', '0', '0', '1', '0', '0', '0', '1', '1');

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '部门编号',
  `name` varchar(255) NOT NULL COMMENT '部门名称',
  `shortcall` varchar(255) DEFAULT NULL,
  `canboundary` int(11) DEFAULT '0',
  `caninland` int(11) DEFAULT '0',
  `canemergency` int(11) DEFAULT '0',
  `canregular` int(11) DEFAULT '0',
  `highdepid` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`),
  KEY `department_ibfk_1` (`highdepid`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('1', '黑龙江省航道局', '省', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('2', '哈总段', '哈', '1', '1', '1', '0', '0');
INSERT INTO `department` VALUES ('3', '佳木斯航道局', '佳', '1', '1', '1', '1', '0');
INSERT INTO `department` VALUES ('4', '黑河航道局', '黑', '1', '0', '1', '1', '0');
INSERT INTO `department` VALUES ('5', '一中心', '一', '0', '0', '1', '1', '0');
INSERT INTO `department` VALUES ('6', '二中心', '二', '0', '0', '1', '1', '0');
INSERT INTO `department` VALUES ('7', '三中心', '三', '0', '0', '1', '1', '0');
INSERT INTO `department` VALUES ('8', '测绘中心', '测', '1', '1', '1', '1', '0');
INSERT INTO `department` VALUES ('9', '信息中心', '信', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('10', '安监室', '安', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('11', '航政科', '政', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('12', '航道科', '道', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('13', '教育科', '教', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('14', '通讯办', '通', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('15', '设备科', '设', '1', '1', '0', '0', '1');
INSERT INTO `department` VALUES ('16', '财务科', '财', '1', '1', '0', '0', '1');

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `position` varchar(255) NOT NULL,
  `departmentid` int(11) NOT NULL,
  `cansubmit` int(11) NOT NULL,
  `cansign` int(11) NOT NULL,
  `isadmin` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `canstatistic` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`),
  KEY `departmentid` (`departmentid`),
  CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`departmentid`) REFERENCES `department` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('1', '成坚', '负责人', '1', '1', '1', '0', 'chengjian', 'chengjian', '1');
INSERT INTO `employee` VALUES ('2', '石啸', '主管', '1', '1', '1', '0', 'shixiao', 'shixiao', '0');
INSERT INTO `employee` VALUES ('3', '张立平', '负责人', '1', '0', '1', '0', 'zhangliping', 'zhangliping', '0');
INSERT INTO `employee` VALUES ('4', '许伟', '科长', '4', '0', '1', '0', 'xuwei', 'xuwei', '0');
INSERT INTO `employee` VALUES ('5', '赵强', '科长', '3', '0', '1', '0', 'zhaoqiang', 'zhaoqiang', '0');
INSERT INTO `employee` VALUES ('6', '李景龙', '科长', '2', '0', '1', '0', 'lijinglong', 'lijinglong', '0');
INSERT INTO `employee` VALUES ('7', '王盼盼', '副局长', '5', '0', '1', '1', 'wangpanpan', 'wangpanpan', '1');
INSERT INTO `employee` VALUES ('8', '吴佳怡', '局长', '5', '0', '1', '1', 'wujiayi', 'wujiayi', '1');
INSERT INTO `employee` VALUES ('13', '111', '111', '1', '0', '0', '0', '111', '111', '0');

-- ----------------------------
-- Table structure for hdjcontract
-- ----------------------------
DROP TABLE IF EXISTS `hdjcontract`;
CREATE TABLE `hdjcontract` (
  `id` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `contempid` int(11) NOT NULL COMMENT '当前会签单所使用的模版类',
  `columndata1` varchar(255) NOT NULL,
  `columndata2` varchar(255) NOT NULL,
  `columndata3` varchar(255) NOT NULL,
  `columndata4` varchar(255) NOT NULL,
  `columndata5` varchar(255) NOT NULL,
  `subempid` int(11) NOT NULL COMMENT '申请人的ID（=0表示申请人与）',
  `submitdate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `contempid` (`contempid`),
  KEY `subempid` (`subempid`),
  CONSTRAINT `hdjcontract_ibfk_1` FOREIGN KEY (`contempid`) REFERENCES `contemp` (`id`),
  CONSTRAINT `hdjcontract_ibfk_2` FOREIGN KEY (`subempid`) REFERENCES `employee` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of hdjcontract
-- ----------------------------
INSERT INTO `hdjcontract` VALUES ('省内2016001', '养护及例会项目拨款会签审批单 ', '1', '内河航道养护工程', '航标养护', '航标巡查   工作量 : 100   投资额 : 100\r\n', '100', '100', '1', '2016-03-09 13:58:42');
INSERT INTO `hdjcontract` VALUES ('省界2016001', '养护及例会项目拨款会签审批单 ', '1', '界河航道养护工程', '航道养护', '航道巡查   工作量 : 100   投资额 : 100\r\n', '100   壹佰元', '100    壹佰元', '1', '2016-03-13 21:46:45');

-- ----------------------------
-- Table structure for item
-- ----------------------------
DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `projectid` int(11) DEFAULT NULL,
  `item` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `projectid` (`projectid`),
  CONSTRAINT `item_ibfk_1` FOREIGN KEY (`projectid`) REFERENCES `project` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=81 DEFAULT CHARSET=utf8;

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
INSERT INTO `item` VALUES ('21', '4', '房屋维护-航标段船架设陆地电缆');
INSERT INTO `item` VALUES ('22', '4', '卧泊基地维护-黑河基地日常养护');
INSERT INTO `item` VALUES ('23', '4', '卧泊基地维护-黑河基地维修改造');
INSERT INTO `item` VALUES ('24', '4', '卧泊基地维护-同江基地日常养护');
INSERT INTO `item` VALUES ('25', '4', '卧泊基地维护-同江基地维修改造');
INSERT INTO `item` VALUES ('26', '4', '卧泊基地维护-漠河基地日常养护');
INSERT INTO `item` VALUES ('27', '4', '卧泊基地维护-佳木斯基地维修改造');
INSERT INTO `item` VALUES ('28', '5', '航道安全保障(暂估、暂列）');
INSERT INTO `item` VALUES ('29', '5', '外事协调');
INSERT INTO `item` VALUES ('30', '5', '政策及技术研究');
INSERT INTO `item` VALUES ('31', '5', '设计');
INSERT INTO `item` VALUES ('32', '5', '监理');
INSERT INTO `item` VALUES ('33', '5', '船员伙食津贴');
INSERT INTO `item` VALUES ('34', '5', '航道养护津贴');
INSERT INTO `item` VALUES ('35', '5', '职工培训费');
INSERT INTO `item` VALUES ('36', '5', '船员冬季劳保用品');
INSERT INTO `item` VALUES ('37', '5', '航道卫生监督费');
INSERT INTO `item` VALUES ('38', '5', '临时用工费');
INSERT INTO `item` VALUES ('39', '5', '执法服装及办公费');
INSERT INTO `item` VALUES ('40', '5', '航道养护专项检查、管理费');
INSERT INTO `item` VALUES ('41', '6', '延伸养护调研费');
INSERT INTO `item` VALUES ('42', '7', '航道巡查');
INSERT INTO `item` VALUES ('43', '7', '航道测量');
INSERT INTO `item` VALUES ('44', '7', '仪器购置及检测费');
INSERT INTO `item` VALUES ('45', '7', '航道疏浚');
INSERT INTO `item` VALUES ('46', '7', '水工整治');
INSERT INTO `item` VALUES ('47', '7', '航道清障');
INSERT INTO `item` VALUES ('48', '7', '水文观测及咨询');
INSERT INTO `item` VALUES ('49', '8', '航标巡查');
INSERT INTO `item` VALUES ('50', '8', '航标保养及维修');
INSERT INTO `item` VALUES ('51', '8', '航标器材购置');
INSERT INTO `item` VALUES ('52', '8', '示位标及三姓9#标');
INSERT INTO `item` VALUES ('53', '8', '94-113航标遥测遥控');
INSERT INTO `item` VALUES ('54', '9', '船体保养');
INSERT INTO `item` VALUES ('55', '9', '机电设备维修');
INSERT INTO `item` VALUES ('56', '9', '船舶检验');
INSERT INTO `item` VALUES ('57', '9', '配件、备件及材料费');
INSERT INTO `item` VALUES ('58', '9', '安全与通讯设备保养');
INSERT INTO `item` VALUES ('59', '9', '水处理');
INSERT INTO `item` VALUES ('60', '10', '站房维护-日常维护');
INSERT INTO `item` VALUES ('61', '10', '站房维护-三姓维修');
INSERT INTO `item` VALUES ('62', '10', '站房维护-绥滨维修');
INSERT INTO `item` VALUES ('63', '10', '卧泊基地维护-哈尔滨基地日常养护');
INSERT INTO `item` VALUES ('64', '10', '卧泊基地维护-佳木斯基地日常养护');
INSERT INTO `item` VALUES ('65', '10', '卧泊基地维护-哈尔滨不间断电源');
INSERT INTO `item` VALUES ('66', '11', '工程质量检测');
INSERT INTO `item` VALUES ('67', '11', '设计费');
INSERT INTO `item` VALUES ('68', '11', '监理费');
INSERT INTO `item` VALUES ('69', '11', '卫生监督检查经费');
INSERT INTO `item` VALUES ('70', '11', '船员伙食津贴');
INSERT INTO `item` VALUES ('71', '11', '船员航行津贴');
INSERT INTO `item` VALUES ('72', '11', '内河劳动保护用品');
INSERT INTO `item` VALUES ('73', '12', '航标');
INSERT INTO `item` VALUES ('74', '13', '测量');
INSERT INTO `item` VALUES ('75', '14', '疏浚');
INSERT INTO `item` VALUES ('76', '15', '水工');
INSERT INTO `item` VALUES ('77', '16', '航标');
INSERT INTO `item` VALUES ('78', '17', '测量');
INSERT INTO `item` VALUES ('79', '18', '疏浚');
INSERT INTO `item` VALUES ('80', '19', '水工');

-- ----------------------------
-- Table structure for project
-- ----------------------------
DROP TABLE IF EXISTS `project`;
CREATE TABLE `project` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `categoryid` int(11) DEFAULT NULL,
  `project` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `categoryid` (`categoryid`),
  CONSTRAINT `project_ibfk_1` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of project
-- ----------------------------
INSERT INTO `project` VALUES ('1', '1', '航道养护');
INSERT INTO `project` VALUES ('2', '1', '航标养护');
INSERT INTO `project` VALUES ('3', '1', '船艇养护');
INSERT INTO `project` VALUES ('4', '1', '站场维护');
INSERT INTO `project` VALUES ('5', '1', '其他');
INSERT INTO `project` VALUES ('6', '1', '延伸养护');
INSERT INTO `project` VALUES ('7', '2', '航道养护');
INSERT INTO `project` VALUES ('8', '2', '航标养护');
INSERT INTO `project` VALUES ('9', '2', '船艇养护');
INSERT INTO `project` VALUES ('10', '2', '站场维护');
INSERT INTO `project` VALUES ('11', '2', '其他');
INSERT INTO `project` VALUES ('12', '3', '航标');
INSERT INTO `project` VALUES ('13', '3', '测量');
INSERT INTO `project` VALUES ('14', '3', '疏浚');
INSERT INTO `project` VALUES ('15', '3', '水工');
INSERT INTO `project` VALUES ('16', '4', '航标');
INSERT INTO `project` VALUES ('17', '4', '测量');
INSERT INTO `project` VALUES ('18', '4', '疏浚');
INSERT INTO `project` VALUES ('19', '4', '水工');

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
  KEY `regularload_ibfk_1` (`itemid`),
  CONSTRAINT `regularload_ibfk_1` FOREIGN KEY (`itemid`) REFERENCES `item` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of regularload
-- ----------------------------
INSERT INTO `regularload` VALUES ('2016-1-1', '1', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-10', '10', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-11', '11', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-12', '12', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-13', '13', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-14', '14', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-15', '15', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-16', '16', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-17', '17', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-18', '18', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-19', '19', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-2', '2', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-20', '20', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-21', '21', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-22', '22', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-23', '23', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-24', '24', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-25', '25', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-26', '26', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-27', '27', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-28', '28', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-29', '29', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-3', '3', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-30', '30', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-31', '31', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-32', '32', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-33', '33', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-34', '34', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-35', '35', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-36', '36', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-37', '37', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-38', '38', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-39', '39', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-4', '4', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-40', '40', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-41', '41', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-5', '5', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-6', '6', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-7', '7', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-8', '8', '100.00', '2000.00', '2016');
INSERT INTO `regularload` VALUES ('2016-1-9', '9', '100.00', '2000.00', '2016');

-- ----------------------------
-- Table structure for signaturedetail
-- ----------------------------
DROP TABLE IF EXISTS `signaturedetail`;
CREATE TABLE `signaturedetail` (
  `date` datetime NOT NULL COMMENT '签字的日期',
  `empid` int(11) NOT NULL COMMENT '签字的人员id',
  `conid` varchar(255) NOT NULL COMMENT '签字的会签单表',
  `result` int(11) NOT NULL COMMENT '签字结果(-1拒绝，0未知,1同意)',
  `remark` varchar(255) NOT NULL COMMENT '签字的备注信息',
  `updatecount` int(11) DEFAULT NULL,
  PRIMARY KEY (`date`,`empid`,`conid`),
  KEY `conid` (`conid`),
  CONSTRAINT `signaturedetail_ibfk_1` FOREIGN KEY (`conid`) REFERENCES `hdjcontract` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of signaturedetail
-- ----------------------------
INSERT INTO `signaturedetail` VALUES ('2016-03-09 13:59:44', '1', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:00:15', '2', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:00:36', '3', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:00:55', '4', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:01:18', '5', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:01:43', '6', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:02:01', '8', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-09 14:02:22', '7', '省内2016001', '1', '未填', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-13 21:34:58', '1', '省界2016001', '-1', '阿凤双飞阿凤洒洒爱色爱色飞洒', '0');
INSERT INTO `signaturedetail` VALUES ('2016-03-13 21:45:54', '1', '省界2016001', '-1', '速度爱色爱色', '1');
INSERT INTO `signaturedetail` VALUES ('2016-03-13 21:46:34', '1', '省界2016001', '-1', '阿发 阿福啊', '2');

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
INSERT INTO `signaturelevel` VALUES ('1', '1', '1', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '2', '2', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '3', '3', '1', '1', '1');
INSERT INTO `signaturelevel` VALUES ('1', '4', '4', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '5', '5', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '6', '6', '1', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '7', '8', '2', '0', '0');
INSERT INTO `signaturelevel` VALUES ('1', '8', '7', '3', '0', '0');

-- ----------------------------
-- Table structure for signaturestatus
-- ----------------------------
DROP TABLE IF EXISTS `signaturestatus`;
CREATE TABLE `signaturestatus` (
  `id` varchar(255) NOT NULL,
  `conid` varchar(255) NOT NULL,
  `result1` int(11) NOT NULL,
  `result2` int(11) NOT NULL,
  `result3` int(11) NOT NULL,
  `result4` int(11) NOT NULL,
  `result5` int(11) NOT NULL,
  `result6` int(11) NOT NULL,
  `result7` int(11) NOT NULL,
  `result8` int(11) NOT NULL,
  `totalresult` int(11) NOT NULL,
  `agreecount` int(11) NOT NULL DEFAULT '0',
  `refusecount` int(11) NOT NULL,
  `currlevel` int(11) NOT NULL COMMENT '当前签字',
  `maxlevel` int(11) NOT NULL COMMENT '完成所需节点',
  `updatecount` int(11) DEFAULT '0',
  `isdownload` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `signaturestatus_ibfk_1` (`conid`),
  CONSTRAINT `signaturestatus_ibfk_1` FOREIGN KEY (`conid`) REFERENCES `hdjcontract` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of signaturestatus
-- ----------------------------
INSERT INTO `signaturestatus` VALUES ('2016-03-09 13:58:42', '省内2016001', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '0', '4', '3', '0', '0');
INSERT INTO `signaturestatus` VALUES ('2016-03-13 21:34:38', '省界2016001', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '3', '3', '0');

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
  PRIMARY KEY (`id`),
  KEY `workload_ibfk_1` (`contractid`),
  KEY `workload_ibfk_2` (`itemid`),
  CONSTRAINT `workload_ibfk_1` FOREIGN KEY (`contractid`) REFERENCES `hdjcontract` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `workload_ibfk_2` FOREIGN KEY (`itemid`) REFERENCES `item` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of workload
-- ----------------------------
INSERT INTO `workload` VALUES ('省内20160017', '省内2016001', '49', '100.00', '100.00');
INSERT INTO `workload` VALUES ('省界20160011', '省界2016001', '1', '100.00', '100.00');

-- ----------------------------
-- Table structure for yhjlhxmbkcontract
-- ----------------------------
DROP TABLE IF EXISTS `yhjlhxmbkcontract`;
CREATE TABLE `yhjlhxmbkcontract` (
  `id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '养护及例会项目拨款会签审批单编号',
  `proname` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT '养护及例会项目拨款会签审批单工程名称',
  `termname` varchar(255) NOT NULL,
  `termsize` varchar(255) NOT NULL COMMENT '养护及例会项目拨款会签审批单主要项目以及工作量',
  `reqcapital` int(11) NOT NULL COMMENT '本次申请资金额度（元）',
  `totalcaptial` int(11) NOT NULL COMMENT '累计申请资金额度（元）',
  `reqdepartproid` int(11) NOT NULL COMMENT '申请单位项目负责人',
  `reqdepartid` int(11) NOT NULL COMMENT '申请单位负责人',
  `condepartproid` int(11) NOT NULL COMMENT '养护主管部门项目负责人（需要签字）',
  `condepartid` int(11) NOT NULL COMMENT '养护主管部门负责人（需要签字）',
  `plandepartid` int(11) NOT NULL COMMENT '计划科负责人（需要签字）',
  `finadepartid` int(11) NOT NULL COMMENT '财务科负责人（需要签字）',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of yhjlhxmbkcontract
-- ----------------------------
DROP TRIGGER IF EXISTS `insert_signature_level`;
DELIMITER ;;
CREATE TRIGGER `insert_signature_level` AFTER INSERT ON `contemp` FOR EACH ROW BEGIN

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
 
END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `update_signature_level`;
DELIMITER ;;
CREATE TRIGGER `update_signature_level` AFTER UPDATE ON `contemp` FOR EACH ROW BEGIN

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


END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `set_conidcategory`;
DELIMITER ;;
CREATE TRIGGER `set_conidcategory` BEFORE UPDATE ON `department` FOR EACH ROW BEGIN
    if (old.canboundary = 0 and new.canboundary = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 1);
    elseif (old.canboundary = 1 and new.canboundary = 0) then 
        DELETE from `conidcategory` 
        WHERE(`departmentid` = new.id and `categoryid` = 1);
    end if;

    if (old.caninland = 0 and new.caninland = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 2);
    elseif (old.caninland = 1 and new.caninland = 0) then 
        DELETE from `conidcategory`
        WHERE(`departmentid` = new.id and `categoryid` = 2);
    end if;
        
    if (old.canemergency = 0 and new.canemergency = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 3);
    elseif (old.canemergency = 1 and new.canemergency = 0) then 
        DELETE from `conidcategory`
        WHERE(`departmentid` = new.id and `categoryid` = 3);
    end if;

    if (old.canregular = 0 and new.canregular = 1) then 
        INSERT into `conidcategory` (`departmentid`, `categoryid`)
        VALUES(new.id, 4);
    elseif (old.canregular = 1 and new.canregular = 0) then 
        DELETE from `conidcategory`
        WHERE(`departmentid` = new.id and `categoryid` = 4);
    end if;
 END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `insert_signature_status`;
DELIMITER ;;
CREATE TRIGGER `insert_signature_status` AFTER INSERT ON `hdjcontract` FOR EACH ROW BEGIN

            INSERT INTO `signaturestatus` (`id`, `conid`, `result1`, `result2`, `result3`, `result4`, `result5`, `result6`, `result7`, `result8`, `totalresult`, `agreecount`, `refusecount`, `currlevel`, `maxlevel`, `updatecount`) 
            VALUES (NOW(), new.id, '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', (SELECT c.signlevel8 FROM `hdjcontract` h, `contemp` c WHERE (h.contempid = c.id and h.id = new.id)), '0');


END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `update_signature_status`;
DELIMITER ;;
CREATE TRIGGER `update_signature_status` AFTER UPDATE ON `hdjcontract` FOR EACH ROW BEGIN

            UPDATE `signaturestatus`
            set `result1` = '0', `result2` = '0', `result3` = '0', `result4` = '0', `result5` = '0', `result6` = '0', `result7` = '0', `result8` = '0', `totalresult` = '0', `agreecount` = '0', `refusecount` = '0', `currlevel` = '1', `updatecount` = `updatecount` + 1
            WHERE (conid = new.id);

        END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `modify_signature_status`;
DELIMITER ;;
CREATE TRIGGER `modify_signature_status` BEFORE INSERT ON `signaturedetail` FOR EACH ROW BEGIN
            set new.updatecount = (SELECT `updatecount` FROM `signaturestatus` WHERE (conid = new.conid));

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
        
        END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `set_signature_status_totalresult`;
DELIMITER ;;
CREATE TRIGGER `set_signature_status_totalresult` BEFORE UPDATE ON `signaturestatus` FOR EACH ROW BEGIN

	
		 if (new.result1 = '1' and new.result2 = '1' and new.result3 = '1' and new.result4 = '1' and new.result5 = '1' and new.result6 = '1' and new.result7 = '1' and new.result8 = '1')

			then set new.totalresult = '1';
	
		elseif (new.result1 = '-1' or new.result2 = '-1' or new.result3 = '-1' or new.result4 = '-1' or new.result5 = '-1' or new.result6 = '-1' or new.result7 = '-1' or new.result8 = '-1')
			
			then set new.totalresult = '-1';

		end if;


END
;;
DELIMITER ;
