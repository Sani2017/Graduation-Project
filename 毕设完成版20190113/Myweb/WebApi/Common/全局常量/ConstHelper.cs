using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConstHelper
    {

        #region 成功
        /// <summary>
        /// 获取列表成功
        /// </summary>
        public const string GET_LIST_SUCCESS = "获取列表成功";
        /// <summary>
        /// 获取成功
        /// </summary>
        public const string GET_MODEL_SUCCESS = "获取成功";
        /// <summary>
        /// 删除成功
        /// </summary>
        public const string DELETE_MODEL_SUCCESS = "删除成功";
        /// <summary>
        /// 更新成功
        /// </summary>
        public const string UPDATE_MODEL_SUCCESS = "更新成功";
        /// <summary>
        /// 新增成功
        /// </summary>
        public const string INSERT_MODEL_SUCCESS = "新增成功";
        /// <summary>
        /// 保存成功
        /// </summary>
        public const string SAVE_MODEL_SUCCESS = "保存成功";
        /// <summary>
        /// 认证通过
        /// </summary>
        public const string AUTHENTICATION_SUCCESS = "认证成功";
        /// <summary>
        /// 注册成功
        /// </summary>
        public const string REGISTER_SUCCESS = "注册成功";
        /// <summary>
        /// 用户登录成功
        /// </summary>
        public const string USER_LOGIN_SUCCESS = "登录成功";
        /// <summary>
        /// 退出登录成功
        /// </summary>
        public const string QUIT_LOGIN_SUCCESS = "退出登录成功";
        /// <summary>
        /// 上传成功
        /// </summary>
        public const string UPLOAD_SUCCESS = "上传成功";
        /// <summary>
        /// 取消成功
        /// </summary>
        public const string CANCEL_SUCCESS = "取消成功";
        /// <summary>
        /// 拒绝成功
        /// </summary>
        public const string REFUSE_SUCCESS = "拒绝成功";
        /// <summary>
        /// 审核成功
        /// </summary>
        public const string CHECK_SUCCESS = "审核成功";
        /// <summary>
        /// 作品未审核
        /// </summary>
        public const string WORK_CHECK_NOT = "作品未审核";
        /// <summary>
        /// 发送成功
        /// </summary>
        public const string SEND_SUCCESS = "发送成功";
        /// <summary>
        /// 入驻成功
        /// </summary>
        public const string AGENCY_SUCCESS = "入驻成功";
        /// <summary>
        /// 意见反馈提交成功
        /// </summary>
        public const string ADVICE_SAVE_SUCCESS = "意见反馈提交成功";
        /// <summary>
        /// 修改成功
        /// </summary>
        public const string MODIFY_SUCCESS = "修改成功";
        #endregion

        #region 失败
        /// <summary>
        /// 意见反馈提交失败
        /// </summary>
        public const string ADVICE_SAVE_ERROR = "意见反馈提交失败";
        /// <summary>
        /// 识别失败
        /// </summary>
        public const string UPLOAD_ERROR = "识别失败";
        /// <summary>
        /// 获取列表失败
        /// </summary>
        public const string GET_LIST_ERROR = "获取列表失败";
        /// <summary>
        /// 获取失败
        /// </summary>
        public const string GET_MODEL_ERROR = "获取失败";
        /// <summary>
        /// 删除失败
        /// </summary>
        public const string DELETE_MODEL_ERROR = "删除失败";
        /// <summary>
        /// 更新失败
        /// </summary>
        public const string UPDATE_MODEL_ERROR = "更新失败";
        /// <summary>
        /// 新增失败
        /// </summary>
        public const string INSERT_MODEL_ERROR = "新增失败";
        /// <summary>
        /// 保存失败
        /// </summary>
        public const string SAVE_MODEL_ERROR = "保存失败";
        /// <summary>
        /// 发送验证码失败
        /// </summary>
        public const string VERIFICATIONCODE_SEND_ERROR = "发送验证码失败";
        /// <summary>
        /// 发送图片失败
        /// </summary>
        public const string PICTURE_SEND_ERROR = "发送图片失败";
        /// <summary>
        /// 取消失败
        /// </summary>
        public const string CANCEL_ERROR = "取消失败";
        /// <summary>
        /// 发送失败
        /// </summary>
        public const string SEND_ERROR = "发送失败";
        /// <summary>
        /// 发送失败
        /// </summary>
        public const string CHECK_ERROR = "发送失败";
        /// <summary>
        /// 生成二维码失败
        /// </summary>
        public const string CREATE_BARCORE_ERROR = "生成二维码失败";
        /// <summary>
        /// 获取access_token失败
        /// </summary>
        public const string GET_ACCESS_TOKEN_ERROR = " 获取access_token失败";
        /// <summary>
        /// 用户信息反序列化失败
        /// </summary>
        public const string USERINFO_TRANSFER_ERROR = "用户信息反序列化失败";

        /// <summary>
        /// 该标题已存在
        /// </summary>
        public const string NEWSTITLE_UNIQUER = "该标题已存在";

        /// <summary>
        /// 修改失败
        /// </summary>
        public const string MODIFY_FAILED = "修改失败";
        #endregion

        #region 请选择
        /// <summary>
        /// 请选择团队组长
        /// </summary>
        public const string CHOOSE_TEAM_LEADER = "请选择团队组长";
        /// <summary>
        /// 请选择机构
        /// </summary>
        public const string CHOOSE_ORG = "请选择机构";
        /// <summary>
        /// 请选择医院
        /// </summary>
        public const string CHOOSE_HOSPITAL = "请选择医院";
        /// <summary>
        /// 请选择角色
        /// </summary>
        public const string CHOOSE_ROLE = "请选择角色";
        /// <summary>
        /// 请选择文件
        /// </summary>
        public const string CHOOSE_FILE = "请选择文件";
        #endregion

        #region 必需
        /// <summary>
        /// 医生编号必需
        /// </summary>
        public const string BIRTHDAY_NEEDED = "生日必需";
        /// <summary>
        /// 审核原因必需
        /// </summary>
        public const string CHECK_REASON_NEEDED = "审核原因必需";
        /// <summary>
        /// 医生编号必需
        /// </summary>
        public const string DOCTOR_ID_NEEDED = "医生编号必需";
        /// <summary>
        /// 服务名称必需
        /// </summary>
        public const string SERVICE_NAME_NEEDED = "服务名称必需";
        /// <summary>
        /// 身份证必需
        /// </summary>
        public const string IDCARD_NEEDED = "身份证必需";
        /// <summary>
        /// 验证码是必需的
        /// </summary>
        public const string VERIFICATION_CODE_NEEDED = "验证码是必需的";
        /// <summary>
        /// 手机号是必需的
        /// </summary>
        public const string PHONE_NEEDED = "手机号是必需的";
        /// <summary>
        /// 密码是必需的
        /// </summary>
        public const string PASSWORD_NEEDED = "密码是必需的";
        /// <summary>
        /// 新密码不能为空
        /// </summary>
        public const string NEW_PASSWORD_NEEDED = "新密码是必需的";
        /// <summary>
        /// ID必需
        /// </summary>
        public const string ID_NEEDE = "ID必需";
        /// <summary>
        /// OPENID必需的
        /// </summary>
        public const string OPENID_NEEDED = "OPENID必需的";
        /// <summary>
        /// 手机或OPENID必需的
        /// </summary>
        public const string PHONE_OR_OPENID_NEEDED = "手机或OPENID必需的";
        /// <summary>
        /// 用户名必需的
        /// </summary>
        public const string USERNAME_NEEDED = "用户名必需的";
        /// <summary>
        /// 用户名或密码必需的
        /// </summary>
        public const string USERNAME_PASSWORD_NEEDED = "用户名或密码必需的";
        /// <summary>
        /// 查询条件是必需的
        /// </summary>
        public const string SELECT_CONDITION_NEEDED = "查询条件是必需的";
        #endregion

        #region 不存在
        /// <summary>
        /// 不存在数据
        /// </summary>
        public const string NO_EXIST_DATA = "不存在数据，请返回重试";
        /// <summary>
        /// 不存在文件
        /// </summary>
        public const string NO_EXIST_OBJECT = "不存在缓存对象，请返回重试";
        /// <summary>
        /// 不存在文件
        /// </summary>
        public const string NO_EXIST_FILE = "不存在文件，请返回重试";
        /// <summary>
        /// 不存在按钮信息
        /// </summary>
        public const string NO_EXIST_BUTTON_INFO = "不存在按钮信息";
        /// <summary>
        /// 不存在菜单信息
        /// </summary>
        public const string NO_EXIST_MENU_INFO = "不存在菜单信息";
        /// <summary>
        /// 不存在医院信息
        /// </summary>
        public const string NO_EXIST_HOSPITAL_INFO = "不存在医院信息";
        /// <summary>
        /// 不存在机构信息
        /// </summary>
        public const string NO_EXIST_ORG_INFO = "不存在机构信息";
        /// <summary>
        /// 不存在用户信息
        /// </summary>
        public const string NO_EXIST_USER_INFO = "不存在用户信息";
        /// <summary>
        /// 不存在地址信息
        /// </summary>
        public const string NO_EXIST_ADDRESS_INFO = "不存在地址信息";
        /// <summary>
        /// 不存在的居民信息
        /// </summary>
        public const string NO_EXIST_RESIDENT_INFO = "不存在的居民信息";
        /// <summary>
        /// 不存在的角色信息
        /// </summary>
        public const string NO_EXIST_ROLE_INFO = "不存在的角色信息";
        /// <summary>
        /// 不存在的成员信息
        /// </summary>
        public const string NO_EXIST_MEMBER_INFO = "不存在的成员信息";
        /// <summary>
        /// 不存在的记录信息
        /// </summary>
        public const string NO_EXIST_RECORD_INFO = "不存在的记录信息";
        /// <summary>
        /// 不存在的图片信息
        /// </summary>
        public const string NO_EXIST_PICTURE_INFO = "不存在的图片信息";
        /// <summary>
        /// 不存在的二维码信息
        /// </summary>
        public const string NO_EXIST_BARCORE_INFO = "不存在的二维码信息";
        /// <summary>
        /// 不存在未读消息
        /// </summary>
        public const string NO_EXIST_NOT_READING_MESSAGE = "不存在未读消息";
        /// <summary>
        /// 不存在的家庭成员
        /// </summary>
        public const string NO_EXIST_NOT_FAMILY_PERSON = "不存在的家庭成员";
        /// <summary>
        /// 不存在的目录
        /// </summary>
        public const string NO_EXIST_NOT_CATALOGUE = "不存在的目录";
        /// <summary>
        /// 不存在的MEDIAID
        /// </summary>
        public const string NO_EXIST_NOT_MEDIAID = "不存在的MEDIAID";
        /// <summary>
        /// 不存在号码信息
        /// </summary>
        public const string NO_EXIST_PHONE = "不存在号码信息";
        #endregion

        #region 已存在
        /// <summary>
        /// 短信模板名称已存在
        /// </summary>
        public const string MESSAGE_MODEL_NAME_EXISTED = "短信模板名称已存在";
        /// <summary>
        /// 类型已存在
        /// </summary>
        public const string SORT_NAME_EXISTED = "类型已存在";
        /// <summary>
        /// 医生工号已存在
        /// </summary>
        public const string DOCTOR_CODE_EXISTED = "医生工号已存在";
        /// <summary>
        /// 号码已存在
        /// </summary>
        public const string PHONE_EXISTED = "手机号码已存在";
        /// <summary>
        /// 身份证号已存在
        /// </summary>
        public const string ID_CARD_EXISTED = "身份证号已存在";
        /// <summary>
        /// 该角色已存在
        /// </summary>
        public const string ROLE_EXISTED = "该角色已存在";
        /// <summary>
        /// 该团队名已存在
        /// </summary>
        public const string DOCTOR_TEAM_EXISTED = "该团队名已存在";
        /// <summary>
        /// 该用户已存在
        /// </summary>
        public const string USER_EXISTED = "该用户已存在";
        /// <summary>
        /// 该手机号已经注册，你可以直接登录
        /// </summary>
        public const string USER_REGISTERED = "该手机号已经注册，你可以直接登录";
        /// <summary>
        /// 存在未读消息
        /// </summary>
        public const string NOT_END_NOT_READING_MESSAGE = "存在未读消息";
        /// <summary>
        /// 已存在订单，请返回重试"
        /// </summary>
        public const string ORDER_EXISTED = "已存在订单，请返回重试";
        /// <summary>
        /// 验证码超时,请重新获取
        /// </summary>
        public const string NO_EXIST_DUANXINCODE = "验证码超时,请重新获取";
        /// <summary>
        /// 存在的成员信息
        /// </summary>
        public const string EXIST_MEMBER_INFO = "已存在的成员信息";
        #endregion

        #region 注册
        /// <summary>
        /// 用户未注册
        /// </summary>
        public const string USER_NOT_REGISTER = "用户未注册";
        /// <summary>
        /// 用户手机号已注册
        /// </summary>
        public const string PHONE_IS_REGISTER = "用户手机号已注册";
        /// <summary>
        /// 身份证号已注册
        /// </summary>
        public const string PATIENT_CARDID_IS_REGISTER = "该身份证号已被注册";
        /// <summary>
        /// 该手机号未注册
        /// </summary>
        public const string PHONE_ISNOT_REGISTER = "该手机号未注册";
        /// <summary>
        /// 用户名已注册
        /// </summary>
        public const string USER_NAME_IS_TREGISTER = "用户名已注册";
        /// <summary>
        /// 邮箱已注册
        /// </summary>
        public const string EMAIL_IS_TREGISTER = "邮箱已注册";
        #endregion

        #region 删除
        /// <summary>
        /// 非图片格式，不能删除
        /// </summary>
        public const string DONOT_DELETE_FORMAT_ERROR = "格式错误，不能删除";
        /// <summary>
        /// 无法删除当前登录用户
        /// </summary>
        public const string DONOT_DELETE_SELF = "无法删除当前登录用户";
        /// <summary>
        /// 系统管理员无法删除
        /// </summary>
        public const string DONOT_DELETE_ADMIN = "系统管理员无法删除";
        /// <summary>
        /// 无权删除角色级别较高的用户
        /// </summary>
        public const string DONOT_DELETE_HIGHER = "无权删除角色级别较高的用户";
        /// <summary>
        /// 不支持批量删除
        /// </summary>
        public const string DONOT_DELETE_LOTS = "不支持批量删除";
        /// <summary>
        /// 超级管理员不支持删除
        /// </summary>
        public const string DONOT_DELETE_SUPERMAN = "超级管理员不支持删除";
        /// <summary>
        /// 该服务包已被签约使用，禁止删除
        /// </summary>
        public const string DONOT_DELETE_SERVICE_PACK = "该服务包已被签约使用，禁止删除或编辑";

        #endregion

        #region 禁用
        /// <summary>
        /// 不能禁用当前登录用户
        /// </summary>
        public const string DONOT_BAN_SELF = "不能禁用当前登录用户";
        /// <summary>
        /// 无权禁用高级别用户
        /// </summary>
        public const string DONOT_BAN_HIGHER = "无权禁用高级别用户";
        #endregion

        #region 错误
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public const string USER_OR_PASSWORD_ERROR = "用户名或密码错误";
        /// <summary>
        /// 密码错误
        /// </summary>
        public const string PASSWORD_ERROR = "密码错误";
        /// <summary>
        /// 原密码错误
        /// </summary>
        public const string OLDPASSWORD_ERROR = "原密码错误";
        /// <summary>
        /// 验证码错误
        /// </summary>
        public const string VERIFICATION_CODE_ERROR = "验证码错误";
        /// <summary>
        /// 验证通过
        /// </summary>
        public const string VERIFICATION_CODE_SUCCESS = "验证通过";
        /// <summary>
        /// 手机号错误
        /// </summary>
        public const string PHONE_ERROR = "手机号错误";
        /// <summary>
        /// 身份证错误
        /// </summary>
        public const string IDCARD_ERROR = "身份证错误";
        /// <summary>
        /// 参数错误
        /// </summary>
        public const string PARAMETER_ERROR = "参数错误";
        /// <summary>
        /// 文件错误
        /// </summary>
        public const string FILE_ERROR = "文件错误";
        /// <summary>
        /// 居民手机号为空或格式错误
        /// </summary>
        public const string PATIENT_PHONR_ERROT = "居民手机号为空或格式错误";
        /// <summary>
        /// 用户名或密码为空
        /// </summary>
        public const string USERNAME_PASSWORD_EMPTY = "用户名或密码为空";
        /// <summary>
        /// 用户token为空
        /// </summary>
        public const string TOKEN_ISNULL = "用户token为空";
        /// <summary>
        /// 没有用户留言
        /// </summary>
        public const string ONLINE_ISNULL = "没有用户留言";
        #endregion

        /// <summary>
        ///无权修改或删除他人创建的短信模版
        /// </summary>
        public const string NO_POWER_UPDATE_MESSAGE_MODEL = "无权修改或删除他人创建的短信模版";
        /// <summary>
        /// 当前用户医院信息不完善
        /// </summary>
        public const string USER_HOSPITAL_INFO_FAULTINESS = "当前用户医院信息不完善";
        /// <summary>
        ///该医院无二维码信息
        /// </summary>
        public const string HOSPITAL_NO_BARCORE = "该医院无二维码信息";

        /// <summary>
        /// 该代金券已下线，请刷新重试
        /// </summary>
        public const string OFF_LINE_CARD = "该代金券已下线，请返回重试";

        /// <summary>
        /// 无权重置高级别用户密码
        /// </summary>
        public const string NO_POWER_TORESET_PASSWORD = "无权重置高级别用户密码";
        /// <summary>
        /// Token过期
        /// </summary>
        public const string TOKEN_TIMEOUT = "Token过期";
        /// <summary>
        /// 用户已禁用
        /// </summary>
        public const string USER_NOT_ALLOW = "用户已禁用";
        /// <summary>
        /// 医生已关联用户
        /// </summary>
        public const string DOCTOR_LINED_USER = "医生已关联用户";
        /// <summary>
        /// 医生未关联用户
        /// </summary>
        public const string DOCTOR_NOT_LINED_USER = "医生未关联用户";
        /// <summary>
        /// 验证码已发送
        /// </summary>
        public const string VERIFICATION_CODE_SEND_SUCESS = "验证码已发送";
        /// <summary>
        /// 请不要频繁发送验证码
        /// </summary>
        public const string VERIFICATION_CODE_SEND_ALREADY = "请不要频繁发送验证码";
        /// <summary>
        /// 新密码与确认密码不一致
        /// </summary>
        public const string DOUBLE_PASSWORD_NOT_SAME = "密码与确认密码不一致";
        /// <summary>
        /// 输入的手机号与绑定的手机号不一致
        /// </summary>
        public const string DOUBLE_PHONE_NOT_SAME = "输入的手机号与绑定的手机号不一致";
        /// <summary>
        /// 该医生已为其它团队组长
        /// </summary>
        public const string DOCTOR_APPOINTED = "该医生已为其它团队组长";
        /// <summary>
        /// 是否将组员的签约数据转移给组长
        /// </summary>
        public const string IS_TRANSFER_MEDICAL_DATA = "是否将组员的签约数据转移给组长";
        /// <summary>
        /// 数据不合法
        /// </summary>
        public const string DATA_NOT_LEGAL = "数据不合法";
        /// <summary>
        /// 未获取到结果
        /// </summary>
        public const string GET_NOTHING = "未获取到结果";
        /// <summary>
        /// 问诊已结束
        /// </summary>
        public const string INQUIRY_END = "问诊已结束";
        /// <summary>
        /// 用户未登录
        /// </summary>
        public const string USER_NOT_LOGIN = "用户未登录";
        /// <summary>
        /// 非签约成员
        /// </summary>
        public const string NOT_MEDICAL_PERSON = "非签约成员";
        /// <summary>
        /// 该医生不是此患者当前服务年度的签约医生
        /// </summary>
        public const string NOT_THIS_YEAR_MEDICAL_DOCTOR = "该医生不是此患者当前服务年度的签约医生";
        /// <summary>
        /// 未入驻
        /// </summary>
        public const string NO_AGENCY = "未入驻";
        /// <summary>
        /// 该微信号已入驻
        /// </summary>
        public const string WX_AGENCY = "该微信号已入驻";
        /// <summary>
        /// 该手机号已入驻
        /// </summary>
        public const string PHONE_AGENCY = "该手机号已入驻";
        /// <summary>
        /// 信息未修改
        /// </summary>
        public const string NOT_MODEL_INFO = "信息未修改";
        /// <summary>
        /// 标题不能为空
        /// </summary>
        public const string NOT_NULL_INFORMATION_TITLE = "标题不能为空";

        /// <summary>
        /// 内容不能为空
        /// </summary>
        public const string NOT_NULL_INFORMATION_CONTENT = "内容不能为空";
        /// <summary>
        /// 分类不能为空
        /// </summary>
        public const string NOT_NULL_SORT_CONTENT = "分类不能为空";
        /// <summary>
        /// 传值不能为空
        /// </summary>
        public const string NOT_NULL_ELEMENT_CONTENT = "传值不能为空";

        /// <summary>
        /// 发布开始时间不能大于发布结束时间
        /// </summary>
        public const string NOT_PUBLICSTARTTIME_GREATER_THAN_PUBLICENDTIME = "发布开始时间不能大于发布结束时间";

        /// <summary>
        /// 发布结束时间不能小于当前时间
        /// </summary>
        public const string NOT_PUBLICENDTIME_LESS_THEN_CURRENT_TIME = "发布结束时间不能小于当前时间";
        /// <summary>
        /// 时间不能为空
        /// </summary>
        public const string TIME_ISNULL = "时间不能为空";
        /// <summary>
        /// 跳转链接输入错误
        /// </summary>
        public const string ERRO_LINKS = "微信端跳转链接必须包含https";

        /// <summary>
        /// 上传内容过长或图片过大
        /// </summary>
        public const string CONTENT_TOO_LARGE_OR_IMGEA_TOO_BIG = "上传内容过长或图片过大";


    }
}
