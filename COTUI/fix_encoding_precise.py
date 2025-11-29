#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os
import re

# 精确的替换映射表
replacements = {
    # DashboardPage.cs 中的具体错误
    'ʧ秒': '失败',
    'ҳ错误�ʧ�ܣ�': '页面加载失败：',
    '�Ǳ错误秒ݼ错误秒': '看板数据加载成功',
    '错误�Ǳ错误请求': '加载看板数据',
    '错误�Ǳ错误请求ʧ秒': '加载看板数据失败',
    '数据加载ʧ�ܣ�': '数据加载失败：',
    '错误ͳ错误秒': '更新统计数据',
    '查询错误错误�': '查询今日数据',
    '错误ͳ�ƿ�Ƭ': '更新统计控件',
    '请求±请求': '更新最近记录',
    '�Ǳ错误秒ݸ请求': '看板数据更新',
    '秒{': '总产量：{',
    ', 秒{': ', 良品：{',
    ', 错误{': ', 不良：{',
    ', 秒Ʒ秒{': ', 良品率：{',
    '错误ͳ错误秒ʧ秒': '更新统计数据失败',
    '错误错误�¼': '更新最近记录',
    'ȡ请求20秒': '取最近20条',
    'δ֪': '未知',
    
    # 其他常见模式
    '����': '错误',
    '�ɹ�': '成功',
    'ʵʱ': '实时',
    '�ֶ�': '字段',
    '���캯��': '构造函数',
    '��ݿ�': '数据库',
    '����': '连接',
    '��ѯ': '查询',
    '��¼': '记录',
    'ִ��': '执行',
    '��ʼ��': '初始化',
    '���': '失败',
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
    '��������': '重新连接',
    '�����': '开始',
    '��ֹ': '停止',
    '��ݿ���ֶ�': '数据库字段',
    '��ݿ�����': '数据库连接',
    '��ݿ����': '数据库操作',
    '��ݿ��ѯ': '数据库查询',
    '��ݿ�¼': '数据库记录',
    '����ʱ��': '生产时间',
    '����Ա': '操作员',
    '��Ʒ����': '产品型号',
    '��Ʒ��': '产品率',
    '��Ʒ��': '良品率',
    '��ͼ': '图表',
    'ͳ�Ʒ���': '统计分析',
    '��ݼ���': '数据加载',
    '����ʧ��': '加载失败',
    '��������': '更新数据',
    '��ʾ': '显示',
    '����': '隐藏',
    '��ʽ': '格式',
    '��ʽ��': '格式化',
    '��Ч��': '有效性',
    '��֤': '验证',
    '���': '异常',
    '����': '处理',
    '��־': '日志',
    '��¼��': '记录器',
}

def fix_file(filepath):
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # 应用所有替换
        for old, new in replacements.items():
            content = content.replace(old, new)
        
        if content != original_content:
            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        return False
    except Exception as e:
        print(f"处理文件 {filepath} 时出错: {e}")
        return False

# 需要修复的文件列表
files_to_fix = [
    './统计分析/DashboardPage.cs',
    './通用功能类/DatabaseLoggerExtension.cs',
    './扫码管理/BarcodeScanPage.cs',
    './扫码管理/ProductSNManager.cs',
    './数据库/DatabaseHelper.cs',
    './数据库/Services/ProductionDataService.cs',
    './数据库/Services/AlarmRecordService.cs',
    './数据库/Services/MotionControlService.cs',
    './数据库/Services/SystemLogService.cs',
    './运动控制页面/MotionControlPage.cs',
]

fixed_count = 0
for filepath in files_to_fix:
    if os.path.exists(filepath):
        if fix_file(filepath):
            fixed_count += 1
            print(f"✓ 已修复: {filepath}")
    else:
        print(f"✗ 文件不存在: {filepath}")

print(f"\n总共修复了 {fixed_count} 个文件")
