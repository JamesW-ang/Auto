#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Fix GBK encoding garbled characters by re-reading as GBK
This script attempts to recover the original Chinese text
"""

import os
import codecs

def fix_gbk_file(file_path):
    """
    Try to fix GBK garbled characters
    Strategy: Read file bytes, try to identify GBK sequences and fix them
    """
    try:
        print(f"\nProcessing: {file_path}")
        
        # Read as binary
        with open(file_path, 'rb') as f:
            raw_bytes = f.read()
        
        # Try to decode as UTF-8 with replacement
        try:
            content_utf8 = raw_bytes.decode('utf-8', errors='replace')
        except:
            print(f"  Cannot decode as UTF-8")
            return False
        
        # Count garbled characters
        garbled_count = content_utf8.count('�')
        
        if garbled_count == 0:
            print(f"  No garbled characters found")
            return False
        
        print(f"  Found {garbled_count} garbled characters (�)")
        
        # Try to fix by attempting GBK decode on problematic sections
        # This is a heuristic approach
        fixed_content = content_utf8
        
        # Common GBK patterns that got corrupted
        replacements = {
            # From analysis of the files
            "串口": "",  # Need context to fix properly
            "请求": "",
            "秒": "",
            # We need a more sophisticated approach
        }
        
        print(f"  Strategy: Manual correction needed for context-specific fixes")
        print(f"  Recommendation: Use an editor with GBK support or restore from GBK backup")
        
        return False
        
    except Exception as e:
        print(f"  Error: {e}")
        return False

def fix_with_manual_mappings(file_path):
    """
    Fix using manual context-based mappings
    This requires knowing what each garbled sequence should be
    """
    try:
        print(f"\nProcessing with manual mappings: {file_path}")
        
        with open(file_path, 'r', encoding='utf-8', errors='replace') as f:
            content = f.read()
        
        original_content = content
        fixes_made = 0
        
        # Based on the context from the files, create specific mappings
        # These are educated guesses based on common Chinese programming terms
        
        # For DatabaseLoggerExtension.cs
        if 'DatabaseLoggerExtension.cs' in file_path:
            mappings = {
                "串口�ļ请求־秒Ϣ": "接收文件日志消息",
                "串口�־串口�ã�ֱ�ӷ请求": "未启用日志功能，直接返回",
                "请求ӵ串口ݿ�д串口�": "加入数据库写入队列",
                "串口Ǳ串口请求Warn, Error, Fatal串口ͬʱд�뱨串口": "如果是告警级别（Warn, Error, Fatal），同时写入报警表",
                "串口秒־�У�CSV秒ʽ秒": "解析日志行（CSV格式）",
                "�ָ�CSV秒": "分割CSV行",
                "秒ʵ�֣请求ȡǰ20请求ַ请求Ϊ串口串口": "简单实现：提取前20个字符作为告警码",
                "串口秒־请求У请求̨�̣߳�": "处理日志队列（后台线程）",
                "�Ӷ串口�ȡ串口־": "从队列取出日志",
                "�ﵽ请求δ�С串口�Ϊ定时串口秒д串口�ݿ�": "达到批次大小或为最后一批时，写入数据库",
                "定时串口�д�д串口־秒Ҳд串口�ݿ�": "超时了但有日志，也写入数据库",
                "秒Ĭ串口串口秒Ӱ串口串口": "错误不影响继续处理",
                "�߳秒˳�ǰ秒д秒ʣ串口־": "线程退出前写入剩余日志",
                "串口д串口�ݿ�": "批量写入数据库",
                "秒Ĭ串口串口秒ѭ串口秒": "错误不影响循环继续",
                "串口д串口־失败": "批量写入日志失败",
                "�ر串口ݿ请求־秒չ秒ILogListener �ӿڣ�": "关闭数据库日志扩展（ILogListener 接口）",
                "ˢ�¶串口�ʣ串口־": "刷新队列剩余日志",
                "ֹͣ秒̨�߳�": "停止后台线程",
                "秒 Logger ע秒": "从 Logger 注销",
                "串口ʱ串口": "调用时机",
                "串口�˳�ʱ串口 Logger.Shutdown() ֮ǰ": "程序退出时在 Logger.Shutdown() 之前",
                "串口�˳�": "程序退出",
                "串口 ˢ�²秒ر�": "步骤3：刷新并关闭",
                "ֹͻ串口串口־": "停止接收日志",
                "�ȴ请求̨�̴߳串口�ʣ串口־": "等待后台线程处理完剩余日志",
                "�ȴ请求̨�߳秒˳�": "等待后台线程退出",
                "ǿ秒ˢ�¶串口е串口请求־串口�ݿ�": "强制刷新队列中的日志到数据库",
                "LogPage 查询ǰȷ串口串口־串口�": "LogPage 查询前确保最新日志已写",
                "串口�˳�ǰǿ秒д秒": "程序退出前强制写入",
                "串口ʱ�鿴实时串口": "调试时查看实时日志",
                "͸串口串口串口串口请求߳�": "同步调用会阻塞线程",
                "串口时间ȡ请求ڶ秒г请求": "调用时间取决于队列长度",
                "串口�ں�̨�̵߳请求": "不影响后台线程工作",
                "ȡ串口串口串口秒־": "取出所有待写日志",
                "д秒ʣ串口�־": "写入剩余日志",
                "ǿ秒ˢ串口ɣ�д秒": "强制刷新完成，写入",
                "ǿ秒刷新失败": "强制刷新失败",
            }
            
            for garbled, correct in mappings.items():
                if garbled in content:
                    content = content.replace(garbled, correct)
                    fixes_made += 1
                    print(f"  Fixed: {garbled[:30]}... -> {correct[:30]}...")
        
        # For other files, add similar mappings...
        
        if fixes_made > 0:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            print(f"  ✓ Made {fixes_made} fixes")
            return True
        else:
            print(f"  No fixes applied")
            return False
            
    except Exception as e:
        print(f"  Error: {e}")
        import traceback
        traceback.print_exc()
        return False

def main():
    files = [
        "/Users/james/Desktop/workspace/Auto/COTUI/通用功能类/DatabaseLoggerExtension.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/扫码管理/BarcodeScanPage.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/扫码管理/ProductSNManager.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/数据库/DatabaseHelper.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/数据库/Services/ProductionDataService.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/数据库/Services/AlarmService.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/数据库/Services/LogService.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/数据库/Services/UserService.cs",
        "/Users/james/Desktop/workspace/Auto/COTUI/运动控制页面/MotionControlPage.cs",
    ]
    
    print("="*80)
    print("GBK Garbled Character Fix Utility")
    print("="*80)
    
    fixed = 0
    for file_path in files:
        if os.path.exists(file_path):
            if fix_with_manual_mappings(file_path):
                fixed += 1
        else:
            print(f"\nFile not found: {file_path}")
    
    print("\n" + "="*80)
    print(f"Summary: {fixed} files fixed")
    print("="*80)

if __name__ == "__main__":
    main()
