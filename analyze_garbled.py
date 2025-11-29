#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Analyze garbled characters and attempt to fix them
"""

import os
import re
import sys

def find_garbled_lines(file_path):
    """Find all lines containing the replacement character �"""
    try:
        with open(file_path, 'r', encoding='utf-8', errors='replace') as f:
            lines = f.readlines()
        
        garbled_lines = []
        for i, line in enumerate(lines, 1):
            if '�' in line:
                garbled_lines.append((i, line.strip()))
        
        return garbled_lines
    except Exception as e:
        print(f"Error reading {file_path}: {e}")
        return []

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
    
    total_garbled = 0
    
    for file_path in files:
        if not os.path.exists(file_path):
            print(f"File not found: {file_path}")
            continue
            
        garbled_lines = find_garbled_lines(file_path)
        if garbled_lines:
            print(f"\n{'='*80}")
            print(f"File: {os.path.basename(file_path)}")
            print(f"Found {len(garbled_lines)} lines with garbled characters")
            print('='*80)
            
            for line_no, line in garbled_lines[:10]:  # Show first 10
                print(f"Line {line_no}: {line}")
            
            if len(garbled_lines) > 10:
                print(f"... and {len(garbled_lines) - 10} more lines")
            
            total_garbled += len(garbled_lines)
    
    print(f"\n\nTotal: {total_garbled} lines with garbled characters across all files")

if __name__ == "__main__":
    main()
