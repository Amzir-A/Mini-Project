new_classes = ['Player', 'SuperAdventure']

for new_class in new_classes:
    with open(f'{new_class}.cs', 'w') as file:
        file.write(f'public class {new_class}\n' + '{\n' + f'\tpublic {new_class}()\n' + '\t{\n\n' + '\t}\n' + '}')
