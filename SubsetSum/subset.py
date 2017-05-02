import itertools
import unittest

import pdb

input_lists = [
    [1, 2, 3],
    [-5, -3, -1, 2, 4, 6],
    [],
    [-1, 1],
    [-97364, -71561, -69336, 19675, 71561, 97863],
    [-53974, -39140, -36561, -23935, -15680, 0],
    [-83314, -82838, -80120, -63468, -62478, -59378, -56958, -50061, -34791, -32264, -21928, -14988, 23767, 24417, 26403, 26511, 36399, 78055],
    [-92953, -91613, -89733, -50673, -16067, -9172, 8852, 30883, 46690, 46968, 56772, 58703, 59150, 78476, 84413, 90106, 94777, 95148],
    [-94624, -86776, -85833, -80822, -71902, -54562, -38638, -26483, -20207, -1290, 12414, 12627, 19509, 30894, 32505, 46825, 50321, 69294],
    [-83964, -81834, -78386, -70497, -69357, -61867, -49127, -47916, -38361, -35772, -29803, -15343, 6918, 19662, 44614, 66049, 93789, 95405],
    [-68808, -58968, -45958, -36013, -32810, -28726, -13488, 3986, 26342, 29245, 30686, 47966, 58352, 68610, 74533, 77939, 80520, 87195],
    [-97162, -95761, -94672, -87254, -57207, -22163, -20207, -1753, 11646, 13652, 14572, 30580, 52502, 64282, 74896, 83730, 89889, 92200],
    [-93976, -93807, -64604, -59939, -44394, -36454, -34635, -16483, 267, 3245, 8031, 10622, 44815, 46829, 61689, 65756, 69220, 70121],
    [-92474, -61685, -55348, -42019, -35902, -7815, -5579, 4490, 14778, 19399, 34202, 46624, 55800, 57719, 60260, 71511, 75665, 82754],
    [-85029, -84549, -82646, -80493, -73373, -57478, -56711, -42456, -38923, -29277, -3685, -3164, 26863, 29890, 37187, 46607, 69300, 84808],
    [-87565, -71009, -49312, -47554, -27197, 905, 2839, 8657, 14622, 32217, 35567, 38470, 46885, 59236, 64704, 82944, 86902, 90487]
]


output = [False, True, False, True, True, True, False, False, False, False, False, True, True, True, True, True,]

def main():
    challenge_output = []
    for input_list in input_lists:
        subset_contains_zero = split_and_add(input_list)
        for a, b in itertools.combinations(input_list, 2):
            if a == 0 or b == 0 or compare(a, b):
                subset_contains_zero = True
                break

        challenge_output.append(subset_contains_zero)

    print(output == challenge_output)

def compare(a, b):
    return a + b == 0

# Stole this from some guy on the internet
def combinations(input_list):
    # runs in real	0m0.685s
    for i in range(1, len(input_list)):
        for combinations in itertools.combinations(input_list, i):
            if sum(combinations) == 0:
                return True

    return False

def split_and_add(input_list):
    negatives = list(filter(lambda x: x < 0, input_list))
    positives = list(filter(lambda x: x > 0, input_list))

    negative_sums = []
    for i in range(1, len(negatives)):
        for neg_combination in itertools.combinations(negatives, i):
            negative_sums.append(sum(neg_combination))

    positive_sums = []
    for i in range(1, len(positives)):
        for pos_combination in itertools.combinations(positives, i):
            positive_sums.append(sum(pos_combination))

    for num in negative_sums:
        if -num in positive_sums:
            return True

    return False

def inefficient_loops(input_list):
    negatives = list(filter(lambda x: x < 0, input_list))
    positives = list(filter(lambda x: x > 0, input_list))
    
    for negative_index, negative_i in enumerate(negatives):
        for negative_sum in itertools.combinations(negatives, len(negatives) - negative_index):
            for positive_index, positive_i in enumerate(positives):
                for positive_sum in itertools.combinations(positives, len(positives) - positive_index):
                    if sum(negative_sum) + sum(positive_sum) == 0:
                        return True

import timeit

if __name__ == "__main__":
    main()
