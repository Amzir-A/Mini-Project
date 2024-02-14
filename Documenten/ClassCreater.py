new_classes = ['Weapon', 'Monster', 'Quest', 'Location']

for new_class in new_classes:
    with open(f'{new_class}.cs', 'w') as file:
        file.write(f'class {new_class}\n' + '{\n' + f'\tpublic {new_class}()\n' + '\t{\n\n' + '\t}\n' + '}')
