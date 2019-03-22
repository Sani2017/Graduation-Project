/*
Navicat MySQL Data Transfer

Source Server         : 毕设
Source Server Version : 80012
Source Host           : 127.0.0.1:3306
Source Database       : tsblog

Target Server Type    : MYSQL
Target Server Version : 80012
File Encoding         : 65001

Date: 2019-01-15 22:51:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for activity
-- ----------------------------
DROP TABLE IF EXISTS `activity`;
CREATE TABLE `activity` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `ActivityImg` varchar(255) DEFAULT NULL COMMENT '活动封面',
  `ActivityName` varchar(50) NOT NULL COMMENT '活动名称',
  `ActivityContent` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '活动简介',
  `ActivityState` int(10) DEFAULT '1' COMMENT '启用状态（1.启用，0.禁用）默认1',
  `ActivityDate` datetime DEFAULT NULL COMMENT '活动发布时间',
  `EndTime` datetime DEFAULT NULL COMMENT '活动结束时间',
  `LikesCount` int(255) DEFAULT '0' COMMENT '赞数',
  `Hits` int(255) DEFAULT '0' COMMENT '浏览量',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of activity
-- ----------------------------
INSERT INTO `activity` VALUES ('1', 'upload/worksImg/07.jpg', '3活动名称3', '123', '0', '2018-09-30 11:32:06', '2019-01-06 20:22:07', '51', '63');
INSERT INTO `activity` VALUES ('3', 'upload/worksImg/09.jpg', '123', '2312', '0', '2018-09-30 11:32:09', '2019-01-06 20:22:11', '11', '23');
INSERT INTO `activity` VALUES ('4', 'upload/worksImg/11.jpg', '2测试标题1', '测试内容1', '0', '2018-09-20 11:32:12', '2019-01-06 20:22:14', '12', '46');
INSERT INTO `activity` VALUES ('5', 'upload/worksImg/25.jpg', '测试标题1', '测试内容1', '0', '2018-10-06 11:32:14', '2019-01-06 20:22:22', '21', '55');
INSERT INTO `activity` VALUES ('6', 'upload/worksImg/13.jpg', '测试标题2', '测试内容3', '0', '2018-09-19 11:32:21', '2019-01-06 20:22:33', '33', '546');
INSERT INTO `activity` VALUES ('7', 'upload/worksImg/08.jpg', '123444', '123', '0', '2018-12-06 22:32:13', '2018-12-25 20:22:41', '44', '46');
INSERT INTO `activity` VALUES ('8', 'upload/worksImg/08.jpg', '活动发布测试', '<p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p><p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p><p>啊啊啊啊啊</p><p>这是活动发布的阿斯達大賽打啊打</p>', '1', '2019-01-01 23:24:33', '2019-01-27 23:24:47', '0', '4');
INSERT INTO `activity` VALUES ('9', 'upload/worksImg/07.jpg', '活动发布测试', '<p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p>', '1', '2019-01-01 23:24:38', '2019-01-31 23:24:51', '0', '0');
INSERT INTO `activity` VALUES ('10', 'upload/worksImg/25.jpg', '钱钱钱钱钱钱 钱钱', '<p>杀杀杀杀杀杀杀杀杀·</p><p>活动介绍中是不能图片上传的</p>', '1', '2019-01-07 23:24:41', '2019-01-17 23:24:55', '0', '0');
INSERT INTO `activity` VALUES ('11', 'upload/worksImg/13.jpg', '钱钱钱钱钱钱 钱钱', '<p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p><p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p><p>啊啊啊啊a\'a啊啊</p>', '1', '2019-01-02 23:24:44', '2019-01-26 23:24:59', '0', '0');
INSERT INTO `activity` VALUES ('12', 'upload/worksImg/08.jpg', '钱钱钱钱钱后钱钱', '<p<p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p><p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p>>超高层对长谷川</p>', '1', '2019-01-08 06:55:10', '2019-01-25 00:00:00', '0', '0');
INSERT INTO `activity` VALUES ('13', '/upload/activityImg/c2f78f04d69d47b39e7cd0bf80020f84.png', '又改了钱gai钱后钱钱', '<p<p>啊啊啊啊啊</p><p>这是活动发布的测试,活動内容的簡介</p>>超高层对长谷川</p>', '0', '2019-01-08 06:57:20', '2019-01-16 00:00:00', '0', '11');

-- ----------------------------
-- Table structure for admin
-- ----------------------------
DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `Id` int(10) NOT NULL AUTO_INCREMENT COMMENT '管理员表ID编号',
  `AdminName` varchar(50) NOT NULL COMMENT '管理员登录名',
  `AdminPwd` varchar(50) NOT NULL COMMENT '管理员密码',
  `AdminRight` int(5) NOT NULL DEFAULT '2' COMMENT '管理员权限（默认:2，一般，1最高）',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of admin
-- ----------------------------
INSERT INTO `admin` VALUES ('1', 'admin', '202cb962ac59075b964b07152d234b70', '1');
INSERT INTO `admin` VALUES ('3', 'undefined', 'a90bae47c83715da453ccc7188ac41a1', '2');
INSERT INTO `admin` VALUES ('4', 'admin4', 'a90bae47c83715da453ccc7188ac41a1', '2');

-- ----------------------------
-- Table structure for online
-- ----------------------------
DROP TABLE IF EXISTS `online`;
CREATE TABLE `online` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PlaceId` int(255) NOT NULL COMMENT '留言地点id',
  `Uid` int(10) NOT NULL COMMENT '留言人id',
  `OnlineContent` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '留言内容',
  `Creatime` datetime NOT NULL COMMENT '留言时间',
  `LikesCount` bigint(20) DEFAULT '0' COMMENT '留言点赞量',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=COMPACT;

-- ----------------------------
-- Records of online
-- ----------------------------
INSERT INTO `online` VALUES ('1', '2', '1', '留的第一条留言', '2018-04-02 20:58:32', '3');
INSERT INTO `online` VALUES ('2', '1', '2', '留的第二条留言', '2018-04-28 15:36:13', '1');
INSERT INTO `online` VALUES ('3', '3', '1', '留的第三条留言', '2018-10-30 17:35:49', '2');
INSERT INTO `online` VALUES ('4', '2', '3', '留的第四条留言', '2018-10-31 14:48:33', '1');
INSERT INTO `online` VALUES ('5', '2', '6', '1111', '2019-01-04 01:29:59', '0');
INSERT INTO `online` VALUES ('6', '2', '6', '留言测试workid2', '2019-01-04 01:46:04', '0');
INSERT INTO `online` VALUES ('7', '2', '6', '测试2-1 work2', '2019-01-04 01:47:39', '0');
INSERT INTO `online` VALUES ('8', '2', '6', '测试留言2-2 work2', '2019-01-04 01:48:50', '0');
INSERT INTO `online` VALUES ('9', '2', '6', '啊实打实的', '2019-01-04 01:49:43', '1');
INSERT INTO `online` VALUES ('10', '2', '6', '我去饿', '2019-01-04 01:50:53', '4');
INSERT INTO `online` VALUES ('11', '1', '1', '留言测试1', '2019-01-07 02:12:42', '0');

-- ----------------------------
-- Table structure for reply
-- ----------------------------
DROP TABLE IF EXISTS `reply`;
CREATE TABLE `reply` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `OnlineId` int(11) NOT NULL COMMENT '留言id',
  `RUid` int(11) NOT NULL COMMENT '回复人id',
  `Recontent` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '回复内容',
  `Retime` datetime DEFAULT NULL COMMENT '回复时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=COMPACT;

-- ----------------------------
-- Records of reply
-- ----------------------------
INSERT INTO `reply` VALUES ('1', '1', '2', '第二人回复第1条留言', '2018-11-14 09:37:15');
INSERT INTO `reply` VALUES ('2', '2', '2', '第二人回复第2条留言', '2018-11-17 09:37:22');
INSERT INTO `reply` VALUES ('3', '1', '1', '第一人回复第1条留言', '2018-10-23 09:37:29');
INSERT INTO `reply` VALUES ('4', '2', '3', '第二人回复第2条留言2次', '2018-11-18 09:37:56');
INSERT INTO `reply` VALUES ('5', '2', '1', '第一人回复第2条留言', '2018-11-01 09:38:14');
INSERT INTO `reply` VALUES ('6', '4', '3', '回復第四条留言1-1', '2018-11-01 13:48:35');
INSERT INTO `reply` VALUES ('7', '4', '1', '回復第四条留言1-2', '2018-11-01 15:34:28');
INSERT INTO `reply` VALUES ('9', '4', '1', '回復第四条留言1-3', '2018-11-01 15:34:28');
INSERT INTO `reply` VALUES ('10', '1', '6', '测试111', '2019-01-04 01:19:26');
INSERT INTO `reply` VALUES ('11', '10', '6', '回复我去饿1', '2019-01-04 12:45:57');
INSERT INTO `reply` VALUES ('12', '11', '1', '回复测试', '2019-01-07 02:13:02');

-- ----------------------------
-- Table structure for sort
-- ----------------------------
DROP TABLE IF EXISTS `sort`;
CREATE TABLE `sort` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '活动ID',
  `SortName` varchar(255) NOT NULL COMMENT '类型名称',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sort
-- ----------------------------
INSERT INTO `sort` VALUES ('1', 'web');
INSERT INTO `sort` VALUES ('2', 'UI');
INSERT INTO `sort` VALUES ('3', '测试');
INSERT INTO `sort` VALUES ('4', '测试修改');
INSERT INTO `sort` VALUES ('6', 'app');
INSERT INTO `sort` VALUES ('7', 'app2');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `Uuid` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT 'ID',
  `Username` varchar(64) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '账号',
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '密码',
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '姓名',
  `Phone` varchar(64) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '电话',
  `Mail` varchar(64) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '邮箱',
  `Sex` varchar(4) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '性别',
  `Number` varchar(64) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT '学号',
  `State` varchar(4) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT 'F' COMMENT '是否校验短信',
  `Able` varchar(4) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT 'T' COMMENT '是否启用'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=COMPACT;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', 'abc', '7', '第一人', '1888888888', '163@163.com', '男', '201810010', 'F', 'T');
INSERT INTO `user` VALUES ('2', 'def', '7', '第二人', '1888888888', '163@163.com', '男', '201810012', 'F', 'T');
INSERT INTO `user` VALUES ('3', 'fng', '7', '第三人', '181888888888', '163@163.com', '男', '201810013', 'F', 'T');

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `Id` int(20) NOT NULL AUTO_INCREMENT COMMENT '用户ID',
  `UserName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '用户名称',
  `PassWord` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '用户密码',
  `ActualName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '真实姓名',
  `UserPhone` bigint(50) NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '用户邮箱（具体邮箱待定）',
  `UserImg` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '../img/default.jpg' COMMENT '用户头像',
  `UserState` int(11) NOT NULL DEFAULT '1' COMMENT '用户状态(1.启用，0.不启用)默认1',
  `Userlabel` varchar(255) DEFAULT NULL COMMENT '用户个性标签',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '上次登录时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES ('1', '用户1', '789', '798', '18365698511', '123123', '/upload/userImg/b322ab4d2e114f5585b87ba2285eb9ed.jpg', '0', '个性标签1', null);
INSERT INTO `userinfo` VALUES ('2', '用户2', '7', '7', '1836', '4', '/upload/userImg/04.jpg', '1', '个性标签2', null);
INSERT INTO `userinfo` VALUES ('3', '用户31', '1', '1', '10101012134', '1', '/upload/userImg/05.jpg', '0', '个性标签3', '2018-10-08 11:12:42');
INSERT INTO `userinfo` VALUES ('5', '注册测试1', 'Sani.3.16', '真实姓名', '1836877', '942596590@qq.com', '/upload/userImg/03.jpg', '1', '个性标签4', null);
INSERT INTO `userinfo` VALUES ('6', '1', '1', '1', '123234', '1', '/upload/userImg/05.jpg', '1', '个性标签5', null);
INSERT INTO `userinfo` VALUES ('9', '80BHDN801', 'SAni.3.16123', null, '1836871111', null, '/upload/userImg/02.jpg', '1', '个性标签6', null);
INSERT INTO `userinfo` VALUES ('10', '04BT68F2', 'SAni.3.16', null, '183687184', null, '/upload/userImg/05.jpg', '0', null, null);
INSERT INTO `userinfo` VALUES ('11', 'N82Z2FL4', 'SAni.4.33', null, '18368718477', null, '/upload/userImg/02.jpg', '0', null, null);

-- ----------------------------
-- Table structure for works
-- ----------------------------
DROP TABLE IF EXISTS `works`;
CREATE TABLE `works` (
  `Id` int(12) NOT NULL AUTO_INCREMENT COMMENT '作品id',
  `WorkImg` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '作品封面',
  `Title` varchar(20) NOT NULL DEFAULT '' COMMENT '标题',
  `Content` varchar(20000) NOT NULL COMMENT '内容简介',
  `FileAddress` varchar(255) DEFAULT NULL COMMENT '文件地址',
  `ActivityId` int(10) DEFAULT NULL COMMENT '活动ID',
  `AuthorId` int(6) DEFAULT '0' COMMENT '作者ID',
  `AuthorName` varchar(50) DEFAULT '' COMMENT '作者姓名',
  `Sort` int(10) DEFAULT NULL COMMENT '作品分类',
  `CreatedAt` datetime DEFAULT NULL COMMENT '创建时间（用户创建）',
  `PublishedAt` datetime DEFAULT NULL COMMENT '发布时间（管理员发布）',
  `IsDeleted` int(1) DEFAULT '0' COMMENT '是否标识已删除[1:是，0:否],默认值:0',
  `AllowShow` int(1) DEFAULT '0' COMMENT '是否允许展示[0:否,1:是],默认值:0',
  `LikesCount` bigint(20) DEFAULT '0' COMMENT '点赞量',
  `Hits` bigint(255) DEFAULT NULL COMMENT '作品点击浏览量',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of works
-- ----------------------------
INSERT INTO `works` VALUES ('1', 'upload/worksImg/07.jpg', '更改1', '更改1223<p>啊是大分da</p><img src=\"http://localhost:6992//upload/userImg/03.jpg\" alt=\"\"><p>sadasdasda</p>', '阿萨', '4', '5', '0', '1', '2018-09-19 16:42:46', '2018-12-03 13:53:09', '0', '1', '622', '36');
INSERT INTO `works` VALUES ('7', 'upload/worksImg/14.jpg', '测试作品6', '123123', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '1', '2', '', '4', '2018-09-01 23:28:15', '2018-12-04 13:24:33', '0', '1', '625', '119');
INSERT INTO `works` VALUES ('10', 'upload/worksImg/14.jpg', '作品11', '111', '11', '1', '1', '', '1', '2019-01-05 17:33:00', '2019-01-05 17:33:04', '0', '0', '0', null);
INSERT INTO `works` VALUES ('11', 'upload/worksImg/12.jpg', '作品2', '22', '12', '2', '1', '', '1', '2019-01-05 17:33:31', '2019-01-05 17:33:34', '0', '0', '0', null);
INSERT INTO `works` VALUES ('13', 'upload/worksImg/12.jpg', '上传测试1', '<p>hjhjhhjk</p><p><img src=\"http://localhost:6992/Upload/img/8f56090ac4db42a08419ba150ecd1e84.png\"></p>', null, '0', '1', '', '1', '2019-01-06 15:33:37', '2019-01-09 16:14:37', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('14', 'upload/worksImg/12.jpg', 'asd', '<p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '0', '1', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('15', 'upload/worksImg/07.jpg', '测试富文本', '<p><span style=\"font-size: x-large;\">aaaaaa</span></p><h1><span style=\"font-size: x-large; font-family: 微软雅黑; font-style: italic; text-decoration-line: underline line-through;\">bb<span style=\"color: rgb(249, 150, 59);\">b</span>bb</span></h1><div><img src=\"http://localhost:6992/Upload/img/241bbfdc05344b0fb71f45e9813bab5c.png\" style=\"text-align: left; max-width: 50%;\"></div><p><br></p>', 'null', '8', '1', '', '3', '2019-01-06 17:13:33', '2019-01-09 16:14:47', '0', '1', '0', '1');
INSERT INTO `works` VALUES ('16', '/upload/userImg/default.png', '用戶1上傳作品', '<h2 style=\"text-align: center;\">這是文章的題目</h2><p>项目背景<br><br>以腾讯游戏与敦煌游戏中共通的“操作方式”、“游戏视角”、“快乐体验”作为核心<br>通过连贯的视觉脉络，带领用户在感受敦煌文化精彩的同时，体会到腾讯游戏为玩家带来的纯粹快乐。<br><br>创意阐述：<br><br>历经千年变迁，敦煌古代游戏至今仍在流传。<br>让古人用他们的娱乐方式出现在我们熟悉的游戏里，感受古今千年人们游戏的“快乐相通”。<br><br>创意：TG-Kai/TG-耗子王/关关/wen0<br>脚本：wen0<br>设计：Łam/Heyxyu<br>插画：飞行猴<br>动画：有邻 / 婉婷 / 亖<br>逐帧：土口<br>剪辑：小闷<br><br>ps：<br>提供两个视频让酷友观看，他们不同点在于一个是带解说配音，另一个是只有音效不带解说，大家可在留言板评论留言，说说你的看法&nbsp;&nbsp;<br></p>', 'null', '8', '1', '', '2', '2019-01-11 23:06:27', '2019-01-11 23:06:27', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('17', 'upload/worksImg/12.jpg', '測試作品2', '<h2 style=\"text-align: center;\">這是文章的題目2</h2><p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '13', '2', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('18', 'upload/worksImg/12.jpg', '測試作品2', '<h2 style=\"text-align: center;\">這是文章的題目2</h2><p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '12', '2', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('19', 'upload/worksImg/07.jpg', '測試作品3', '<h2 style=\"text-align: center;\">這是文章的題目3</h2><p><span style=\"font-size: x-large;\">aaaaaa</span></p><h1><span style=\"font-size: x-large; font-family: 微软雅黑; font-style: italic; text-decoration-line: underline line-through;\">bb<span style=\"color: rgb(249, 150, 59);\">b</span>bb</span></h1><div><img src=\"http://localhost:6992/Upload/img/241bbfdc05344b0fb71f45e9813bab5c.png\" style=\"text-align: left; max-width: 50%;\"></div><p><br></p>', 'null', '11', '3', '', '3', '2019-01-06 17:13:33', '2019-01-09 16:14:47', '0', '1', '1', '1');
INSERT INTO `works` VALUES ('20', '/upload/userImg/default.png', '测试111', '<p><div style=\"text-align: center;\"><span style=\"font-weight: bold; font-size: 1.6rem;\">标题1</span></div><img src=\"http://localhost:6992/Upload/img/3043c648693b40899406fd7af5ce02b7.jpg\">', 'null', '0', '1', '', '2', '2019-01-12 08:58:58', '2019-01-12 08:58:58', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('21', 'upload/worksImg/07.jpg', '测试富文本-2', '<p><span style=\"font-size: x-large;\">aaaaaa</span></p><h1><span style=\"font-size: x-large; font-family: 微软雅黑; font-style: italic; text-decoration-line: underline line-through;\">bb<span style=\"color: rgb(249, 150, 59);\">b</span>bb</span></h1><div><img src=\"http://localhost:6992/Upload/img/241bbfdc05344b0fb71f45e9813bab5c.png\" style=\"text-align: left; max-width: 50%;\"></div><p><br></p>', 'null', '8', '1', '', '3', '2019-01-06 17:13:33', '2019-01-09 16:14:47', '0', '1', '0', '1');
INSERT INTO `works` VALUES ('22', '/upload/userImg/default.png', '用戶1上傳作品', '<h2 style=\"text-align: center;\">這是文章的題目</h2><p>项目背景<br><br>以腾讯游戏与敦煌游戏中共通的“操作方式”、“游戏视角”、“快乐体验”作为核心<br>通过连贯的视觉脉络，带领用户在感受敦煌文化精彩的同时，体会到腾讯游戏为玩家带来的纯粹快乐。<br><br>创意阐述：<br><br>历经千年变迁，敦煌古代游戏至今仍在流传。<br>让古人用他们的娱乐方式出现在我们熟悉的游戏里，感受古今千年人们游戏的“快乐相通”。<br><br>创意：TG-Kai/TG-耗子王/关关/wen0<br>脚本：wen0<br>设计：Łam/Heyxyu<br>插画：飞行猴<br>动画：有邻 / 婉婷 / 亖<br>逐帧：土口<br>剪辑：小闷<br><br>ps：<br>提供两个视频让酷友观看，他们不同点在于一个是带解说配音，另一个是只有音效不带解说，大家可在留言板评论留言，说说你的看法&nbsp;&nbsp;<br></p>', 'null', '8', '1', '', '2', '2019-01-11 23:06:27', '2019-01-11 23:06:27', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('23', 'upload/worksImg/12.jpg', '測試作品2', '<h2 style=\"text-align: center;\">這是文章的題目2</h2><p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '13', '2', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('24', '/upload/userImg/default.png', '用戶1上傳作品-2', '<h2 style=\"text-align: center;\">這是文章的題目</h2><p>项目背景<br><br>以腾讯游戏与敦煌游戏中共通的“操作方式”、“游戏视角”、“快乐体验”作为核心<br>通过连贯的视觉脉络，带领用户在感受敦煌文化精彩的同时，体会到腾讯游戏为玩家带来的纯粹快乐。<br><br>创意阐述：<br><br>历经千年变迁，敦煌古代游戏至今仍在流传。<br>让古人用他们的娱乐方式出现在我们熟悉的游戏里，感受古今千年人们游戏的“快乐相通”。<br><br>创意：TG-Kai/TG-耗子王/关关/wen0<br>脚本：wen0<br>设计：Łam/Heyxyu<br>插画：飞行猴<br>动画：有邻 / 婉婷 / 亖<br>逐帧：土口<br>剪辑：小闷<br><br>ps：<br>提供两个视频让酷友观看，他们不同点在于一个是带解说配音，另一个是只有音效不带解说，大家可在留言板评论留言，说说你的看法&nbsp;&nbsp;<br></p>', 'null', '8', '1', '', '2', '2019-01-11 23:06:27', '2019-01-11 23:06:27', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('25', 'upload/worksImg/12.jpg', '測試作品2-2', '<h2 style=\"text-align: center;\">這是文章的題目2</h2><p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '13', '2', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('26', 'upload/worksImg/12.jpg', '測試作品2-2', '<h2 style=\"text-align: center;\">這是文章的題目2</h2><p>dads</p>', '/upload/file/54b6114595574c76abf6be4813919b11.zip', '12', '2', '', '1', '2019-01-06 16:00:57', '2019-01-09 16:14:43', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('27', 'upload/worksImg/07.jpg', '測試作品3-2', '<h2 style=\"text-align: center;\">這是文章的題目3</h2><p><span style=\"font-size: x-large;\">aaaaaa</span></p><h1><span style=\"font-size: x-large; font-family: 微软雅黑; font-style: italic; text-decoration-line: underline line-through;\">bb<span style=\"color: rgb(249, 150, 59);\">b</span>bb</span></h1><div><img src=\"http://localhost:6992/Upload/img/241bbfdc05344b0fb71f45e9813bab5c.png\" style=\"text-align: left; max-width: 50%;\"></div><p><br></p>', 'null', '11', '3', '', '3', '2019-01-06 17:13:33', '2019-01-09 16:14:47', '0', '1', '0', '1');
INSERT INTO `works` VALUES ('28', '/upload/userImg/default.png', '测试111-2', '<p><div style=\"text-align: center;\"><span style=\"font-weight: bold; font-size: 1.6rem;\">标题1</span></div><img src=\"http://localhost:6992/Upload/img/3043c648693b40899406fd7af5ce02b7.jpg\">', 'null', '0', '1', '', '2', '2019-01-12 08:58:58', '2019-01-12 08:58:58', '0', '0', '0', '0');
INSERT INTO `works` VALUES ('29', 'upload/worksImg/07.jpg', '测试富文本-2', '<p><span style=\"font-size: x-large;\">aaaaaa</span></p><h1><span style=\"font-size: x-large; font-family: 微软雅黑; font-style: italic; text-decoration-line: underline line-through;\">bb<span style=\"color: rgb(249, 150, 59);\">b</span>bb</span></h1><div><img src=\"http://localhost:6992/Upload/img/241bbfdc05344b0fb71f45e9813bab5c.png\" style=\"text-align: left; max-width: 50%;\"></div><p><br></p>', 'null', '8', '1', '', '3', '2019-01-06 17:13:33', '2019-01-09 16:14:47', '0', '1', '1', '2');
INSERT INTO `works` VALUES ('30', '/upload/userImg/default.png', '上传测试0112', '<p style=\"text-align: center;\"><span style=\"font-weight: bold;\">测试内容</span></p><p style=\"text-align: center;\"><img src=\"http://localhost:6992/Upload/img/a27c7c2cacda43ebafeb8b5bc062a05d.jpg\">', 'null', '0', '1', '', '2', '2019-01-12 13:39:57', '2019-01-12 13:39:57', '0', '0', '0', '0');
