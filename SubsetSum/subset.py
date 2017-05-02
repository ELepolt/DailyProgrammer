import itertools
import unittest

import pdb

input_lists = [[1, 2, 3], [-5, -3, -1, 2, 4, 6], [], [-1, 1], [-97364, -71561, -69336, 19675, 71561, 97863], [-53974, -39140, -36561, -23935, -15680, 0]]
output = [False, False, False, True, True, True]

def main():
    challenge_output = []
    for input_list in input_lists:
        subset_contains_zero = False
        for a, b in itertools.combinations(input_list, 2):
            if compare(a, b):
                subset_contains_zero = True
                break

        challenge_output.append(subset_contains_zero)

    print(output == challenge_output)

def compare(a, b):
    return a + b == 0
   
if __name__ == "__main__":
    main()


