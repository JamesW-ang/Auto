#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os
import re

# 完整的GBK乱码映射
replacements = {
    # 从实际文件中提取的模式
    '�ؼ�ж秒': '控件事件',
    '�󶨵错误�ʱ错误错误': '绑定到父容器时加载数据',
    '请求ݿ请求־秒չ秒': '数据库日志扩展类',
    '服务־ͬ秒д秒': '日志同步写入',
    '请求ݿ�': '数据库',
    '服务请求ԣ�': '主要特性：',
    'ʵ秒': '实现',
    '�ӿڣ秒淶服务־服务': '接口，规范日志格式',
    '服务ɨ服务�ҳ秒': '条码扫描页面',
    '请求ܣ�': '功能：',
    '֧秒3秒ɨ秒ģʽ服务秒': '支持3种扫码模式：自动',
    '服务': '串口',
    '�ֶ请求': '手动输入',
    '秒ƷSN服务服务�򻯰棩': '产品SN管理器（简化版）',
    'SN�ظ服务': 'SN重复检查',
    '请求ݿ秒ļ�·服务服务请求ͷĿ¼�µ�': '数据库文件路径（与主文件同一目录下的',
    '�ļ秒У�': '文件夹）',
    '请求ݿ�·秒': '数据库路径',
    '�ļ服务�': '文件存在',
    
    # 通用GBK to UTF-8 映射
    '����': '错误',
    '�ɹ�': '成功',
    'ʧ��': '失败',
    'ʧ秒': '失败',
    'ʵʱ': '实时',
    '�ֶ�': '字段',
    '���캯��': '构造函数',
    '��ݿ�': '数据库',
    '����': '连接',
    '��ѯ': '查询',
    '��¼': '记录',
    'ִ��': '执行',
    '��ʼ��': '初始化',
    '��ȡ': '获取',
    '����': '更新',
    '��Ч': '有效',
    '��Ч': '无效',
    '��Ϣ': '信息',
    '���': '警告',
    '��ͼ': '试图',
    '��¼': '登录',
    '�û�': '用户',
    '����': '密码',
    '��վ': '工位',
    '��Ʒ': '产品',
    '���': '生产',
    '����': '配置',
    '�����': '参数',
    'ɨ��': '扫码',
    '����': '添加',
    '�޸�': '修改',
    'ɾ��': '删除',
    '����': '保存',
    '����': '取消',
    '�ر�': '关闭',
    '��־': '日志',
    '�쳣': '异常',
    '����': '处理',
    '��ʱ': '超时',
    '�ӿ�': '接口',
    '��Ա': '成员',
    '����': '函数',
    '����': '方法',
    '����': '属性',
    '�¼�': '事件',
    '��������': '重新连接',
    '�����': '开始',
    '��ֹ': '停止',
    '��ʾ': '显示',
    '����': '隐藏',
    '��ʽ': '格式',
    '��֤': '验证',
    '�ļ�': '文件',
    '�ļ���': '文件夹',
    '·��': '路径',
    '��վ': '站点',
    '���': '页面',
    '����': '窗口',
    '�ؼ�': '控件',
    '��ť': '按钮',
    '�б�': '列表',
    '����': '表格',
    'ͳ��': '统计',
    '����': '分析',
}

def clean_file(filepath):
    """清理文件中的乱码"""
    try:
        with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
            content = f.read()
        
        original = content
        
        # 应用所有替换
        for old, new in replacements.items():
            content = content.replace(old, new)
        
        # 删除剩余的无法识别字符（保留ASCII和中文）
        # 这一步要小心，只删除真正的乱码字符
        # content = re.sub(r'[^\x00-\x7F\u4e00-\u9fa5\u3000-\u303f\uff00-\uffef\s]', '', content)
        
        if content != original:
            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        return False
    except Exception as e:
        print(f"Error processing {filepath}: {e}")
        return False

files = [
    './统计分析/DashboardPage.cs',
    './通用功能类/DatabaseLoggerExtension.cs',
    './扫码管理/BarcodeScanPage.cs',
    './扫码管理/ProductSNManager.cs',
    './数据库/DatabaseHelper.cs',
    './数据库/Services/UserService.cs',
    './数据库/Services/AlarmService.cs',
    './数据库/Services/ProductionDataService.cs',
    './数据库/Services/LogService.cs',
    './运动控制页面/MotionControlPage.cs',
]

fixed = 0
for f in files:
    if os.path.exists(f):
        if clean_file(f):
            fixed += 1
            print(f"✓ {f}")
        else:
            print(f"- {f} (无需修改)")
    else:
        print(f"✗ 文件不存在: {f}")

print(f"\n共修复 {fixed} 个文件")
