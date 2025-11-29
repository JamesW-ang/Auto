#!/usr/bin/env python3
# -*- coding: utf-8 -*-

with open('./数据库/DatabaseHelper.cs', 'r', encoding='utf-8', errors='ignore') as f:
    content = f.read()

# 额外的映射
extra_mappings = {
    '条条条ݿ�文件存在�ڣ条ȴ条�': '如果数据库文件不存在，先创建',
    '创建告警表�ݱ�': '创建生产数据表',
    '条�β�ѯ条条�־条': '单次查询最大日志数',
    '�Ƿ条条条Զ条条�': '是否启用自动备份',
    'ͼ�񱣴�·条': '图像保存路径',
    'ִ�зǲ�ѯSQL条条': '执行非查询SQL语句',
    'ִ�зǲ�ѯSQL条��ⲿ条�ã�': '执行非查询SQL语句（外部调用）',
    'ִ�в�ѯ条条�ص条�ֵ': '执行查询并返回单个值',
    'ִ�в�ѯ条条条 DataTable': '执行查询并返回 DataTable',
    '条条创建日志表条条条�õı表已创建': '清理旧的日志数据（保留指定的保留天数）',
    'ɾ条条�ڱ条条条Ѵ条条ģ�': '删除早于保留期限的数据',
    '条条条�ݿ�': '备份数据库',
    '条条条�ݿ�ʧ条': '备份数据库失败',
    '�Ż条条ݿ⣨ѹ条条�ؽ条条条�': '优化数据库（压缩和重建索引）',
    '�Ż条条ݿ�ʧ条': '优化数据库失败',
}

for old, new in extra_mappings.items():
    content = content.replace(old, new)

with open('./数据库/DatabaseHelper.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("✓ 最终修复完成")

# 验证
import subprocess
result = subprocess.run(['grep', '-c', '�', './数据库/DatabaseHelper.cs'], capture_output=True, text=True)
remaining = int(result.stdout.strip()) if result.returncode == 0 else 0
print(f"剩余乱码字符: {remaining}")
