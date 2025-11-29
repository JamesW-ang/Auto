#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os
import re

# 完整的GBK乱码到UTF-8的映射
fixes = {
    # namespace fixes
    '���ݿ�': '数据库',
    'ɨ�����': '扫码管理',
    'ͨ�ù�����': '通用功能类',
    'ͳ�Ʒ���': '统计分析',
    '�˶�����ҳ��': '运动控制页面',
    
    # Common Chinese words
    '������': '辅助类',
    '��ȡ': '获取',
    '����ʵ��': '单例实例',
    '���ݿ��ļ�·��': '数据库文件路径',
    '��������ͬһĿ¼�µ�': '（放在同一目录下的',
    '�ļ��У�': '文件夹）',
    '���ݿ�·��': '数据库路径',
    '�ļ�����': '文件存在',
    '��ȷ����ṹ����': '确保表结构存在',
    '���ݿ���ʼ���ɹ�': '数据库初始化成功',
    '��ʼ��ʧ��': '初始化失败',
    '��ջ': '堆栈',
    
    # DatabaseLoggerExtension
    '�첽������룬������100��/��': '异步批量写入，性能：100条/秒',
    '�Զ����� CSV ��ʽ��־': '自动导出 CSV 格式日志',
    'Warn/Error/Fatal �����Զ��������': 'Warn/Error/Fatal 级别自动发送告警',
    '���ع�գ�ȷ��������־�߳�': '优雅关闭，确保所有日志线程安全',
    '�ܹ����̣�': '架构流程：',
    '�� (ʵ�� ILogListener.OnLog)': '→ (实现 ILogListener.OnLog)',
    '�� (��־���Ӷӽ�)': '→ (日志加入队列)',
    '�� (��̨�߳�������)': '→ (后台线程批量处理)',
    '�� (����д�����ݿ�)': '→ (批量写入数据库)',
    'ʹ�÷�ʽ��': '使用方式：',
    '// Program.cs �г�ʼ��': '// Program.cs 中初始化',
    '// �����˳�ʱ': '// 程序退出时',
    '��������': '性能指标：',
    'OnLog ����ʱ�䣺~0.1ms�����������ӣ�': 'OnLog 调用时间：~0.1ms（无阻塞延迟）',
    '����д��ʱ�䣺~10ms/�����100����': '批量写入时间：~10ms/批（100条）',
    '�����������10,000 ��': '队列最大容量：10,000 条',
    '�ڴ�ռ�ã�~5MB��������': '内存占用：~5MB（满载时）',
    
    # BarcodeScanPage
    'SN��ʽ��֤': 'SN格式验证',
    'ɨ������¼': '扫描历史记录',
    'ʵʱ��־��ʾ': '实时日志提示',
    '���캯��': '构造函数',
    '������ɨ��ҳ�洴���': '条码扫描页面创建完成',
    '��ʼ������': '初始化事件',
    '��ʼ��': '初始化',
    
    # ProductSNManager
    '��ƷSN�����������ģʽ��': '产品SN管理器（单例模式）',
    'SN�ظ����': 'SN重复检测',
    'SN���ݱ���': 'SN数据保存',
    'SN���ݲ�ѯ': 'SN数据查询',
    'ͳ�Ʒ���': '统计分析',
    '����ģʽ': '单例模式',
}

def fix_file(filepath):
    try:
        with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
            content = f.read()
        
        original = content
        for old, new in fixes.items():
            content = content.replace(old, new)
        
        if content != original:
            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(content)
            return True
        return False
    except Exception as e:
        print(f"Error processing {filepath}: {e}")
        return False

files = [
    './数据库/DatabaseHelper.cs',
    './数据库/Services/ProductionDataService.cs',
    './数据库/Services/AlarmService.cs',
    './数据库/Services/LogService.cs',
    './数据库/Services/UserService.cs',
    './运动控制页面/MotionControlPage.cs',
]

fixed = 0
for f in files:
    if os.path.exists(f):
        if fix_file(f):
            fixed += 1
            print(f"✓ {f}")
        else:
            print(f"- {f} (无变化)")
    else:
        print(f"✗ {f} (不存在)")

print(f"\n修复了 {fixed} 个文件")
