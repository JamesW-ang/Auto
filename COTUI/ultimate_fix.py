#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import os
import codecs

# 所有可能的乱码映射
gbk_mappings = [
    # Namespaces
    ('���ݿ�', '数据库'),
    ('ɨ�����', '扫码管理'),
    ('ͨ�ù�����', '通用功能类'),
    ('ͳ�Ʒ���', '统计分析'),
    ('�˶�����ҳ��', '运动控制页面'),
    ('�˶串口�ҳ秒', '运动控制页面'),
    
    # Common phrases
    ('������', '辅助类'),
    ('��ȡ', '获取'),
    ('����ʵ��', '单例实例'),
    ('���ݿ��ļ�·��', '数据库文件路径'),
    ('��������ͼһĿ¼�µ�', '（放在同一目录下的'),
    ('�ļ��У�', '文件夹）'),
    ('���ݿ�·��', '数据库路径'),
    ('�ļ�����', '文件存在'),
    ('��ȷ����ṹ����', '确保表结构存在'),
    ('���ݿ���ʼ���ɹ�', '数据库初始化成功'),
    ('��ʼ��ʧ��', '初始化失败'),
    ('��ջ', '堆栈'),
    ('串口请求ݷ串口ࣨ秒ǿ秒', '生产数据服务（单例）'),
    ('֧串口秒׷�ݣ�', '支持字段追踪'),
    ('串口串口请求ݣ请求ǿ秒', '添加生产数据（单条）'),
    ('֧串口串口�ֶΣ�', '支持事务字段'),
    ('串口�˶串口ƣ�X/Y/Z�ᣩ', '控制运动轴（X/Y/Z轴）'),
    ('֧�־请求/请求/JOG串口�˶�ģʽ', '支持绝对/相对/JOG三种运动模式'),
    ('实时秒状态请求', '实时位置状态'),
    ('秒λ串口�Ϳ秒ٶ�λ', '复位回零和快速定位'),
    ('串口ʱ�䣨CT串口秒', '节拍时间（CT）测试'),
    ('秒ȫ请求ԣ�', '安全特性：'),
    ('秒λ串口串口串口λ秒', '复位需先回零位'),
    ('秒ͣ串口', '急停保护'),
    ('�ŷ串口串口', '防撞机制'),
    ('秒λȷ秒', '定位确认'),
    ('串口�Ż请求', '性能优化：'),
    ('�ӳټ秒أ串口串口�提示秒~50ms秒', '延迟加载，初始化提示约50ms'),
    ('�첽开始串口�ؼ秒ں�̨请求أ串口请求UI秒', '异步初始化控件在后台加载，不阻塞UI'),
    ('状态秒أ�ʹ秒 Task 请求 Timer', '状态轮询使用 Task 替代 Timer'),
    ('�ӳټ秒ر�־', '延迟加载标志'),
    ('串口�˶串口ҳ秒', '运动控制页面'),
    ('��ʼ��', '初始化'),
    ('��ʼ������', '初始化事件'),
    ('���캯��', '构造函数'),
    ('����ģʽ', '单例模式'),
]

def fix_content(text):
    for old, new in gbk_mappings:
        text = text.replace(old, new)
    return text

files_to_fix = [
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

fixed = 0
for filepath in files_to_fix:
    if not os.path.exists(filepath):
        print(f"✗ {filepath} 不存在")
        continue
    
    try:
        with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
            content = f.read()
        
        new_content = fix_content(content)
        
        if new_content != content:
            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(new_content)
            fixed += 1
            print(f"✓ {filepath}")
        else:
            print(f"- {filepath} (已是最新)")
    except Exception as e:
        print(f"✗ {filepath}: {e}")

print(f"\n✓ 修复了 {fixed} 个文件")

# 验证还有多少文件包含乱码字符
print("\n正在验证...")
remaining = 0
for filepath in files_to_fix:
    if os.path.exists(filepath):
        try:
            with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
                content = f.read()
            if '�' in content:
                remaining += 1
                print(f"⚠ {filepath} 仍有乱码")
        except:
            pass

if remaining == 0:
    print("\n✅ 所有文件已清理完成！")
else:
    print(f"\n⚠ 还有 {remaining} 个文件包含乱码")
