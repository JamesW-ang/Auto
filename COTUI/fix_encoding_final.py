#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os

replacements = {
    # DashboardPage.cs specific errors
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
}

files_to_fix = [
    './统计分析/DashboardPage.cs',
    './通用功能类/DatabaseLoggerExtension.cs',
    './扫码管理/BarcodeScanPage.cs',
    './扫码管理/ProductSNManager.cs',
    './数据库/DatabaseHelper.cs',
    './数据库/Services/ProductionDataService.cs',
    './数据库/Services/AlarmService.cs',
    './数据库/Services/LogService.cs',
    './数据库/Services/UserService.cs',
    './运动控制页面/MotionControlPage.cs',
]

fixed_count = 0
for filepath in files_to_fix:
    if os.path.exists(filepath):
        try:
            with open(filepath, 'r', encoding='utf-8') as f:
                content = f.read()
            original_content = content
            for old, new in replacements.items():
                content = content.replace(old, new)
            if content != original_content:
                with open(filepath, 'w', encoding='utf-8') as f:
                    f.write(content)
                fixed_count += 1
                print(f"✓ {filepath}")
        except Exception as e:
            print(f"✗ {filepath}: {e}")
    else:
        print(f"✗ 文件不存在: {filepath}")

print(f"\n修复了 {fixed_count} 个文件")
